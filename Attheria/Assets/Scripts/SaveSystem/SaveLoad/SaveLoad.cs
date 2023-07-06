using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SaveSystem.Surrogates;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private string SavePath => $"{Application.persistentDataPath}/save.lol";
    
    [ContextMenu("Save")]
    public void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }
    [ContextMenu("Load")]
    public void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }

    void CaptureState(IDictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    void RestoreState(IReadOnlyDictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }

    Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            return new Dictionary<string, object>();
        }

        using FileStream stream = File.Open(SavePath, FileMode.Open);
        return (Dictionary<string, object>)getBinaryFormatter().Deserialize(stream);
    }
    
    void SaveFile(object state)
    {
        using var stream = File.Open(SavePath, FileMode.Create);
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