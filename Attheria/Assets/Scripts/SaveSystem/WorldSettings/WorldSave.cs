using System;
using SaveSystem.WorldSettings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldSave : MonoBehaviour
{
    [HideInInspector]  public string Path;

    public string SaveName;
    public string MapName;

    public WorldSettings WorldSettings;

    [SerializeField] private TMP_Text SaveNameText;
    [SerializeField] private TMP_Text MapNameText;
    private void Start()
    {
        SaveNameText.text = SaveName;
        MapNameText.text = MapName;
    }

    public void LoadSave()
    {
        
    }

    public void ShowWorldSaveSettings()
    {
        
    }

    public void DeleteSave()
    {
        
    }
}
