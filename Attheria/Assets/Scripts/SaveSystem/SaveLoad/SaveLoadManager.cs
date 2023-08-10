using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Mirror;
using Misc.Console;
using SaveSystem.SaveLoad;
using SaveSystem.Surrogates;
using UnityEngine;

// ReSharper disable All

public class SaveLoadManager : NetworkBehaviour
{
    public static SaveLoadManager Instance;
    public event DataLoadedDelegate DataLoaded;
    [SyncVar]
    public bool Loaded = false;

    public bool Loading;
    public bool Saving;

    public delegate void DataLoadedDelegate();

    public List<Saveable> Savables; //List of all GameObjects that manage data saving

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        if (!isServer) return;
        Load();
    }

    /// <summary>
    /// Invoke event when all data is loaded
    /// </summary>
    private void onDataLoaded()
    {
        DataLoaded?.Invoke();
        Loaded = true;
        Loading = false;
        Debug.Log("Data loaded!".Bold().Color("#FFFFFF").Size(13));
    }


    //private string SavePath => $"{Application.persistentDataPath}/";

    [ContextMenu("Save")]
    public async void Save()
    {
        if (Saving) return;
        Debug.Log("Saving");

        Saving = true;
        Dictionary<string, Dictionary<string, object>> states = new();
        Dictionary<string, object> state = new();
        foreach (var saveable in Savables)
        {
            //state = LoadFile(saveable.SaveFile);         ///Need more testing to see if performance is better - when not using LoadFile we create new Dictionary every time and we have to write all data again
            state[saveable.Id] = await saveable.CaptureState();
            if (states.ContainsKey(saveable.SaveFile))
            {
                states[saveable.SaveFile].Concat(state);
            }
            else
            {
                states.Add(saveable.SaveFile, state);
            }
        }

        foreach (var v in states)
        {
            SaveFile(v.Value, v.Key);
        }
        Debug.Log("Save done");
        Saving = false;
    }

    [ContextMenu("Load")]
    public async void Load()
    {
        if (Loading) return;
        Debug.Log("Loading");
        Loading = true;
        Dictionary<string, Dictionary<string, object>> states = new();
        foreach (var saveable in Savables)
        {
            var state = LoadFile(saveable.SaveFile);

            if (states.ContainsKey(saveable.SaveFile))
            {
                states[saveable.SaveFile].Concat(state);
            }
            else
            {
                states.Add(saveable.SaveFile, state);
            }

            foreach (var v in states)
            {
                if (v.Value.TryGetValue(saveable.Id, out object value))
                {
                    await saveable.RestoreState(value);
                }
            }
            states.Clear();
        }
        Debug.Log("Loading done");
        onDataLoaded();
    }

    Dictionary<string, object> LoadFile(string path)
    {
        if (!File.Exists($"{GameConfigManager.Instance.SavePath}/{path}"))
        {
            return new Dictionary<string, object>();
        }

        using FileStream stream = File.Open($"{GameConfigManager.Instance.SavePath}/{path}", FileMode.Open);
        return (Dictionary<string, object>)getBinaryFormatter().Deserialize(stream);
    }

    void SaveFile(object state, string path)
    {
        using FileStream stream = File.Open($"{GameConfigManager.Instance.SavePath}/{path}", FileMode.Create);
        getBinaryFormatter().Serialize(stream, state);
    }

    BinaryFormatter getBinaryFormatter()
    {
        BinaryFormatter formatter = new();

        SurrogateSelector selector = new();
        Vector3Surrogate vector3Surrogate = new();
        QuaternionSurrogate quaternionSurrogate = new();

        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        formatter.SurrogateSelector = selector;

        return formatter;
    }

    public void CreateSaveMetaData()
    {
        SaveMetaData meta = new()
        {
            SaveName = MenuManager.Instance.SelectedSaveName,
            SaveDate = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
            Playtime = TimeManager.Instance.PlayTime,
            Version = GameManager.Instance.GameVersion
        };

        string json = JsonUtility.ToJson(meta);
        File.WriteAllText($"{GameConfigManager.Instance.SavePath}/MetaData.dat", json);
    }
}