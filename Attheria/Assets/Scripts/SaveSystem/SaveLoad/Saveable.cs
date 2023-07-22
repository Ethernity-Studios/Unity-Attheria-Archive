using System;
using System.Collections.Generic;
using UnityEngine;

public class Saveable : MonoBehaviour
{
    public string Id;
    public string SaveFile;

    [ContextMenu("Generate Id")]
    private void GenerateId() => Id = Guid.NewGuid().ToString();

    public object CaptureState()
    {
        var state = new Dictionary<string, object>();

        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.SaveData();
        }
        return state;
    }

    public void RestoreState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if (stateDictionary.TryGetValue(typeName, out object value))
            {
                saveable.LoadData(value);
            }
        }
    }
}