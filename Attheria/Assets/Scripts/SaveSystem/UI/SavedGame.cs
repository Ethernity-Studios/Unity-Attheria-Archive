using System;
using System.IO;
using SaveSystem.WorldSettings;
using TMPro;
using UnityEngine;

public class SavedGame : MonoBehaviour
{
    public string Path;

    public string SaveName;
    public string SaveDate;
    public string SavePlaytime;

    [SerializeField] private TMP_Text SaveNameText;
    [SerializeField] private TMP_Text SaveDateText;
    [SerializeField] private TMP_Text SavePlaytimeText;

    [HideInInspector] public SavedWorld savedWorld;
    
    private void Start()
    {
        SaveNameText.text = SaveName;
        SaveDateText.text = SaveDate;
        SavePlaytimeText.text = SavePlaytime;
    }

    public void LoadSave()
    {
        
    }

    public void DeleteSave()
    {
        Debug.Log(Path);

        Directory.Delete(Path, true);
        savedWorld.Saves.Remove(gameObject);
        Destroy(gameObject);
    }
}
