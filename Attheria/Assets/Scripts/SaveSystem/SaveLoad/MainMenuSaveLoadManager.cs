using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SaveSystem.WorldSettings;
using UnityEngine;

public class MainMenuSaveLoadManager : MonoBehaviour
{
    public static MainMenuSaveLoadManager Instance { get; set; }

    [SerializeField] private GameObject Save;

    private string SavePath => $"{Application.persistentDataPath}/Saves";

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        manageSaveFolder();
    }

    void manageSaveFolder()
    {
        if (!Directory.Exists($"{Application.persistentDataPath}/Saves"))
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/Saves");
        }
    }

    public void LoadSaves()
    {
        // ReSharper disable once CollectionNeverQueried.Local
        List<WorldSettings> Saves = new();
        foreach (var d in Directory.GetDirectories(SavePath))
        {
            WorldSettings settings;
            string path = $"{d}/WorldSettings.config";
            if (!File.Exists(path))
            {
                settings= createDefaultSettings();
            }
            else
            {
                string json = File.ReadAllText(path);
                settings = JsonConvert.DeserializeObject<WorldSettings>(json);
            }

            if (settings != null) Saves.Add(settings);
            instantiateSave(settings, d);
        }
    }

    void instantiateSave(WorldSettings setting, string path)
    {
        GameObject g = Instantiate(Save, MainMenuUIManager.Instance.SavedGames.transform, true);
        g.transform.localScale = Vector3.one;
        var ws = g.GetComponent<WorldSave>();
        ws.SaveName = path.Split("/").Last().Split("\\")[1];
        ws.MapName = setting.WorldName;
        ws.Path = path;
        ws.WorldSettings = setting;
    }

    public void CreateSave(WorldSettings settings, string saveName)
    {
        manageSaveFolder();
        int index = 0;
        foreach (var d in Directory.GetDirectories(SavePath))
        {
            if (d == $"{SavePath}\\{saveName}" || d == $"{SavePath}\\{saveName} ({index})") index++;
        }

        saveName = index == 0 ? $"{saveName}" : $"{saveName} ({index})";

        string savePath = $"{SavePath}/{saveName}";
        Directory.CreateDirectory(savePath);
        var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText($"{savePath}/WorldSettings.config", json);

        Directory.CreateDirectory($"{savePath}/Data");
    }

    WorldSettings createDefaultSettings()
    {
        return new WorldSettings()
        {
            WorldName = DefaultWorldSettings.WorldName,
            TestField = DefaultWorldSettings.TestField,
            TestFieldInt = DefaultWorldSettings.TestFieldInt
        };
    }
}