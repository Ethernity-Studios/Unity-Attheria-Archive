using System.Collections.Generic;
using System.IO;
using System.Linq;
using SaveSystem.WorldSettings;
using Tommy;
using UnityEngine;
using UnityEngine.TestTools;

public class MainMenuSaveLoadManager : MonoBehaviour
{
    public static MainMenuSaveLoadManager Instance { get; set; }
    public const int x = 1;

    [SerializeField] private GameObject World;

    private string SavePath => $"{Application.persistentDataPath}/Saves";

    public List<WorldSettings> SavedGames = new();
    public List<GameObject> Saves = new();

    public WorldSettings LoadedSettings;
    public string LoadedWorldPath;

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

    /// <summary>
    /// Checks if save directory exists, if not creates a new one
    /// </summary>
    void manageSaveFolder()
    {
        if (!Directory.Exists($"{Application.persistentDataPath}/Saves"))
        {
            Directory.CreateDirectory($"{Application.persistentDataPath}/Saves");
        }
    }

    /// <summary>
    /// Loads all worlds
    /// </summary>
    public void LoadSaves()
    {
        foreach (var d in Directory.GetDirectories(SavePath))
        {
            WorldSettings settings;
            string path = $"{d}/WorldSettings.wrld";
            if (!File.Exists(path))
            {
                settings = createDefaultSettings();
            }
            else
            {
                using StreamReader reader = File.OpenText(path);
                TomlTable table = TOML.Parse(reader);
                settings = getTomlSettings(table);
            }

            if (settings != null) SavedGames.Add(settings);
            instantiateSave(settings, d);
        }
    }

    /// <summary>
    /// Spawns world gameobject and sets its parameters
    /// </summary>
    /// <param name="setting">Loade world settings</param>
    /// <param name="path">Path to loaded world</param>
    void instantiateSave(WorldSettings setting, string path)
    {
        GameObject g = Instantiate(World, MainMenuUIManager.Instance.SavedWorlds.transform, true);
        g.transform.localScale = Vector3.one;
        Saves.Add(g);
        var ws = g.GetComponent<SavedWorldInstance>();
        ws.WorldName = path.Split("/").Last().Split("\\")[1];
        ws.MapName = setting.world.MapName;
        ws.Path = path;
        ws.WorldSettings = setting;
    }

    /// <summary>
    /// Creates world save file with world settings
    /// </summary>
    /// <param name="settings"></param>
    public void CreateSave(WorldSettings settings)
    {
        manageSaveFolder();
        int index = 0;
        foreach (var d in Directory.GetDirectories(SavePath))
        {
            if (d == $"{SavePath}\\{settings.world.WorldName}" || d == $"{SavePath}\\{settings.world.WorldName} ({index})") index++;
        }

        settings.world.WorldName = index == 0 ? $"{settings.world.WorldName}" : $"{settings.world.WorldName} ({index})";

        string savePath = $"{SavePath}/{settings.world.WorldName}";
        Directory.CreateDirectory(savePath);


        using (StreamWriter writer = File.CreateText($"{savePath}/WorldSettings.wrld"))
        {
            createTomlTable(settings).WriteTo(writer);
        }

        Directory.CreateDirectory($"{savePath}/Data");
    }

    /// <summary>
    /// Generates default world settings
    /// </summary>
    /// <returns></returns>
    WorldSettings createDefaultSettings()
    {
        return new WorldSettings()
        {
            world = new()
            {
                WorldName = DefaultWorldSettings.WorldName,
                MapName = DefaultWorldSettings.MapName
            },
            someSettings = new()
            {
                TestField = DefaultWorldSettings.TestField,
                TestFieldInt = DefaultWorldSettings.TestFieldInt,
            }
        };
    }

    /// <summary>
    /// Updates world settings
    /// </summary>
    /// <param name="settings">New world settings</param>
    /// <param name="path">Path to world settings</param>
    public void OverrideWorldSettings(WorldSettings settings, string path)
    {
        if (!File.Exists($"{path}/WorldSettings.wrld")) return; // World settings file doesn't exists
        File.WriteAllText($"{path}/WorldSettings.wrld", "");
        using StreamWriter writer = File.CreateText($"{path}/WorldSettings.wrld");
        {
            createTomlTable(settings).WriteTo(writer);
        }
    }

    /// <summary>
    /// Reload all world saves
    /// </summary>
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

    /// <summary>
    /// Creates TomlNode with world settings
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    TomlNode createTomlTable(WorldSettings settings) => TomLoader.writeValue(settings);

    /// <summary>
    /// Returns WorldSettings
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    WorldSettings getTomlSettings(TomlNode table) => TomLoader.readValue<WorldSettings>(table);
}