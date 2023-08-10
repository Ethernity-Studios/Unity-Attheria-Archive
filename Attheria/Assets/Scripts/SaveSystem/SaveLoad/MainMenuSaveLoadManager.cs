using System.Collections.Generic;
using System.IO;
using System.Linq;
using MainMenu;
using Mirror;
using SaveSystem.WorldSettings;
using Tommy;
using UnityEngine;

public class MainMenuSaveLoadManager : MonoBehaviour
{
    public static MainMenuSaveLoadManager Instance { get; set; }

    [SerializeField] private GameObject World;

    private string SavePath => $"{Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'))}/Worlds";

    public List<WorldSettings> SavedGames = new();
    public List<GameObject> WorldInstances = new();
    public List<GameObject> SaveInstances = new();

    public WorldSettings LoadedSettings;
    public string LoadedWorldPath;
    public string LoadedSavePath;

    [SerializeField] private Transport KCP_TRANSPORT;
    [SerializeField] private Transport STEAMWORKS_TRANSPORT;

    public bool SteamMode;
    [SerializeField] private SteamLobby steamLobby;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        manageSaveFolder();
    }

    /// <summary>
    /// Checks if save directory exists, if not creates a new one
    /// </summary>
    void manageSaveFolder()
    {
#if !UNITY_SERVER
        if (!Directory.Exists($"{Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'))}/Worlds"))
        {
            Directory.CreateDirectory($"{Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'))}/Worlds");
        }
#endif
    }

    /// <summary>
    /// Loads all worlds
    /// If world contains no saves it will delete world directory
    /// </summary>
    public void LoadSaves()
    {
        foreach (var d in Directory.GetDirectories(SavePath))
        {
            if (Directory.GetDirectories($"{d}/Saves").Length == 0)
            {
                Directory.Delete(d, true);
                continue;
            }

            WorldSettings settings;
            string path = $"{d}/WorldSettings.wrld";
            if (!File.Exists(path))
            {
                settings = CreateDefaultSettings();
            }
            else
            {
                using StreamReader reader = File.OpenText(path);
                TomlTable table = TOML.Parse(reader);
                settings = GetTomlSettings(table);
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
        WorldInstances.Add(g);
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
            CreateTomlTable(settings).WriteTo(writer);
        }

        Directory.CreateDirectory($"{savePath}/Saves");

        LoadedSettings = settings;
        LoadedWorldPath = savePath;

        LoadSave();
    }

    /// <summary>
    /// Generates default world settings
    /// </summary>
    /// <returns></returns>
    public WorldSettings CreateDefaultSettings()
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
            CreateTomlTable(settings).WriteTo(writer);
        }
    }

    /// <summary>
    /// Reload all world saves
    /// </summary>
    public void ReloadSaves()
    {
        SavedGames.Clear();

        foreach (var s in WorldInstances)
        {
            Destroy(s);
        }

        WorldInstances.Clear();
        LoadSaves();
    }

    /// <summary>
    /// Creates TomlNode with world settings
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public TomlNode CreateTomlTable(WorldSettings settings) => TomLoader.writeValue(settings);

    /// <summary>
    /// Returns WorldSettings
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public WorldSettings GetTomlSettings(TomlNode table) => TomLoader.readValue<WorldSettings>(table);

    /// <summary>
    /// Starts selected game
    /// </summary>
    public void LoadSave()
    {
        GameConfigManager.Instance.Settings = LoadedSettings;
        GameConfigManager.Instance.WorldPath = LoadedWorldPath;
        GameConfigManager.Instance.SavePath = LoadedSavePath;
        
        if (SteamMode) SteamLobby.Instance.HostSteamLobby();
        else NetworkManager.singleton.StartHost();
        
        MainMenuUIManager.Instance.ShowLoadingScreen();
    }

    /// <summary>
    /// Sets network transport protocol
    /// </summary>
    /// <param name="onlineMode"></param>
    public void SetTransportProtocol(bool onlineMode)
    {
        NetworkManager.singleton.transport = onlineMode ? STEAMWORKS_TRANSPORT : KCP_TRANSPORT;
        Transport.active = NetworkManager.singleton.transport;
        SteamMode = onlineMode ? STEAMWORKS_TRANSPORT : KCP_TRANSPORT;
    }
}