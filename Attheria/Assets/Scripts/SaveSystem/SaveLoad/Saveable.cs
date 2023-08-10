using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Saveable : MonoBehaviour
{
    public string Id;
    public string SaveFile;

    [ContextMenu("Generate Id")]
    private void GenerateId() => Id = Guid.NewGuid().ToString();

    public async Task<object> CaptureState()
    {
        var state = new Dictionary<string, object>();

        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = await saveable.SaveData();
        }

        return state;
    }

    public Task RestoreState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();

            if (stateDictionary.TryGetValue(typeName, out object value))
            {
                return saveable.LoadData(value);
            }
        }

        return Task.CompletedTask;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Generates id
    /// Adds this Saveable to SaveLoadManager
    /// </summary>
    private void OnValidate()
    {
        if (EditorApplication.isPlaying && !Application.isEditor) return;
        if (Id == string.Empty) GenerateId(); //Generate id when adding component
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager != null && !saveLoadManager.Savables.Contains(this)) saveLoadManager.Savables.Add(this);
    }

    /// <summary>
    /// Removes this Saveable from SaveLoadManager
    /// </summary>
    private void OnDestroy()
    {
        if (EditorApplication.isPlaying && !Application.isEditor) return;
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager != null && saveLoadManager.Savables.Contains(this)) saveLoadManager.Savables.Remove(this);
    }
#endif
}