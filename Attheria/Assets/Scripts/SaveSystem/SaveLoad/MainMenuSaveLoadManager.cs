using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SaveSystem.WorldSettings;
using UnityEngine;

public class MainMenuSaveLoadManager : MonoBehaviour
{
    public static MainMenuSaveLoadManager Instance { get; set; }

    [SerializeField] private GameObject World;

    private string SavePath => $"{Application.persistentDataPath}/Saves";

    public List<WorldSettings> SavedGames = new();
    public List<GameObject> Saves = new();

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
        foreach (var d in Directory.GetDirectories(SavePath))
        {
            WorldSettings settings;
            string path = $"{d}/WorldSettings.wrld";
            if (!File.Exists(path))
            {
                settings= createDefaultSettings();
            }
            else
            {
                string json = File.ReadAllText(path);
                settings = JsonConvert.DeserializeObject<WorldSettings>(json);
            }

            if (settings != null) SavedGames.Add(settings);
            instantiateSave(settings, d);
        }
    }

    void instantiateSave(WorldSettings setting, string path)
    {
        GameObject g = Instantiate(World, MainMenuUIManager.Instance.SavedWorlds.transform, true);
        g.transform.localScale = Vector3.one;
        Saves.Add(g);
        var ws = g.GetComponent<SavedWorld>();
        ws.WorldName = path.Split("/").Last().Split("\\")[1];
        ws.MapName = setting.MapName;
        ws.Path = path;
        ws.WorldSettings = setting;
    }

    public void CreateSave(WorldSettings settings)
    {
        manageSaveFolder();
        int index = 0;
        foreach (var d in Directory.GetDirectories(SavePath))
        {
            if (d == $"{SavePath}\\{settings.WorldName}" || d == $"{SavePath}\\{settings.WorldName} ({index})") index++;
        }

        settings.WorldName = index == 0 ? $"{settings.WorldName}" : $"{settings.WorldName} ({index})";

        string savePath = $"{SavePath}/{settings.WorldName}";
        Directory.CreateDirectory(savePath);
        var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText($"{savePath}/WorldSettings.wrld", json);

        Directory.CreateDirectory($"{savePath}/Data");
    }

    WorldSettings createDefaultSettings()
    {
        return new WorldSettings()
        {
            WorldName = DefaultWorldSettings.WorldName,
            TestField = DefaultWorldSettings.TestField,
            TestFieldInt = DefaultWorldSettings.TestFieldInt,
            MapName = DefaultWorldSettings.MapName
        };
    }

    public void ReloadSaves()
    {
        SavedGames.Clear();

        foreach (var s in Saves)
        {
           Destroy(s); 
        }
        
        Saves.Clear();
        LoadSaves();
    }
}