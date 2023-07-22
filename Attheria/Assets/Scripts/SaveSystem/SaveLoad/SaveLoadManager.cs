using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Mirror;
using SaveSystem.Surrogates;
using UnityEngine;
// ReSharper disable All

public class SaveLoadManager : NetworkBehaviour
{
    public static SaveLoadManager Instance;

    public event DataLoadedDelegate DataLoaded;
    public delegate void DataLoadedDelegate();

    public List<Saveable> Savables; //List of all GameObjects that manage data saving
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Invoke(nameof(onDataLoaded), 1f);
    }

    /// <summary>
    /// Invoke event when all data is loaded
    /// </summary>
    private void onDataLoaded()
    {
        DataLoaded?.Invoke();
        Debug.Log("Data loaded!");
    }
    
    
    private string SavePath => $"{Application.persistentDataPath}/";

    [ContextMenu("Save")]
    public void Save()
    {
        Dictionary<string, object> state = new();
        foreach (var saveable in Savables)
        {
            //state = LoadFile(saveable.SaveFile);         ///Need more testing to see if performance is better - when not using LoadFile we create new Dictionary every time and we have to write all data again
            state[saveable.Id] = saveable.CaptureState();
            SaveFile(state, saveable.SaveFile);
        }
    }

    [ContextMenu("Load")]
    public void Load()
    {
        Dictionary<string, object> state = new();
        foreach (var saveable in Savables)
        {
            state = LoadFile(saveable.SaveFile);
            if (state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }

    Dictionary<string, object> LoadFile(string path)
    {
        if (!File.Exists(SavePath))
        {
            return new Dictionary<string, object>();
        }

        using FileStream stream = File.Open($"{SavePath}{path}.dat", FileMode.Open);
        return (Dictionary<string, object>)getBinaryFormatter().Deserialize(stream);
    }
    
    void SaveFile(object state, string path)
    {
        using var stream = File.Open($"{SavePath}{path}.dat", FileMode.Create);
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
}

public class Sa
{
    public string Id;
    public string SaveFile;
}