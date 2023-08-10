using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Mirror;
using SaveSystem.WorldSettings;
using Tommy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviour
{
    // ReSharper disable once UnassignedField.Global
    public static ServerManager Instance;
#if UNITY_SERVER
    readonly string serverPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));


    public ServerConfigSettings ConfigSettings;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += onChangeScene;
        
        manageServerWorld();
        setupConfig();
        setupServer();
        Debug.Log("Starting server");
        //SceneManager.LoadScene("Attheria");
    }

    /// <summary>
    /// Handles world saves on server build
    /// </summary>
    void manageServerWorld()
    {
        string worldPath = $"{serverPath}/World";
        if (!Directory.Exists(worldPath))
        {
            Debug.Log("World directory not found, creating a new one!");

            Directory.CreateDirectory(worldPath);
        }

        if (!File.Exists($"{worldPath}/WorldSettings.wrld"))
        {
            WorldSettings settings = MainMenuSaveLoadManager.Instance.CreateDefaultSettings();
            using StreamWriter writer = File.CreateText($"{worldPath}/WorldSettings.wrld");
            TomLoader.writeValue(settings).WriteTo(writer);
        }
        
        if(!Directory.Exists($"{worldPath}/Data")) Directory.CreateDirectory($"{worldPath}/Data");
        if(!Directory.Exists($"{worldPath}/Autosaves")) Directory.CreateDirectory($"{worldPath}/Autosaves");

        if (!File.Exists($"{serverPath}/Whitelist.txt"))
        {
            AdminList list = new()
            {
                Ids = new()
            };
            string json = JsonUtility.ToJson(list);
            File.WriteAllText($"{serverPath}/Whitelist.txt", json);
        }

        if (!File.Exists($"{serverPath}/Banlist.txt"))
        {
            BanList list = new();
            string json = JsonUtility.ToJson(list);
            File.WriteAllText($"{serverPath}/Banlist.txt", json);
        }

        if (!File.Exists($"{serverPath}/Adminlist.txt"))
        {
            AdminList list = new();
            string json = JsonUtility.ToJson(list);
            File.WriteAllText($"{serverPath}/Adminlist.txt", json);
        }

        if (!File.Exists($"{serverPath}/Server.cfg"))
        {
            ServerConfigSettings configSettings = new();
            File.WriteAllText($"{serverPath}/Server.cfg", "");
            using StreamWriter writer = File.CreateText($"{serverPath}/Server.cfg");
            {
                TomLoader.writeValue(configSettings).WriteTo(writer);
            }
        }
    }


    /// <summary>
    /// Initiate game config manager
    /// </summary>
    void setupConfig()
    {
        using StreamReader worldReader = File.OpenText($"{serverPath}/World/WorldSettings.wrld");
        TomlTable worldTable = TOML.Parse(worldReader);
        WorldSettings worldSettings = MainMenuSaveLoadManager.Instance.GetTomlSettings(worldTable);

        using StreamReader serverReader = File.OpenText($"{serverPath}/Server.cfg");
        TomlTable serverTable = TOML.Parse(serverReader);
        ServerConfigSettings serverSettings = TomLoader.readValue<ServerConfigSettings>(serverTable);

        GameConfigManager.Instance.Settings = worldSettings;
        GameConfigManager.Instance.ServerSettings = serverSettings;
        GameConfigManager.Instance.WorldPath = serverPath;
        GameConfigManager.Instance.SavePath = $"{serverPath}/World/Data";

        GameConfigManager.Instance.WhitelistedIds = JsonUtility.FromJson<WhiteList>(File.ReadAllText($"{serverPath}/Whitelist.txt")).Ids;
        GameConfigManager.Instance.BannedIds = JsonUtility.FromJson<BanList>(File.ReadAllText($"{serverPath}/Banlist.txt")).Ids;
        GameConfigManager.Instance.AdminIds = JsonUtility.FromJson<AdminList>(File.ReadAllText($"{serverPath}/Adminlist.txt")).Ids;
    }

    void setupServer()
    {
        NetworkManager.singleton.maxConnections = ConfigSettings.MaxPlayersVIP + 20; //Set max server connections to vip slots + 20 for full server, banned players, no whitelisted
    }

    void onChangeScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("OnChangeScene");

        if (scene.buildIndex == 0) return;
        manageServerSave();
        StartCoroutine(ServerAutoSave());
    }

    void manageServerSave()
    {
        if (!Directory.Exists($"{serverPath}/World/Data")) Directory.CreateDirectory($"{serverPath}/World/Data/Saves");

        if (Directory.GetFiles($"{serverPath}/World/Data").Length == 0) StartCoroutine(createFirstSave()); //No save file found - new game
    }

    IEnumerator createFirstSave()
    {
        while (SaveLoadManager.Instance == null)
        {
            yield return null;
        }
        SaveLoadManager.Instance.CreateSaveMetaData();
        SaveLoadManager.Instance.Save();
    }

    IEnumerator ServerAutoSave()
    {
        Debug.Log("ServerAutoSaveCoroutine");

        while (SaveLoadManager.Instance == null)
        {
            yield return null;
        }

        while (true)
        {
            yield return new WaitForSeconds(GameConfigManager.Instance.ServerSettings.SaveInterval);

            if (SaveLoadManager.Instance.Loading || SaveLoadManager.Instance.Saving) yield return null;
        
            if (Directory.GetFiles($"{serverPath}/World/Data").Length != 0)
            {
                Debug.Log("Creating backup");

                string fileName = $"save-{DateTime.Now}".Replace("/", "-").Replace(" ", "-").Replace(".","-").Replace(":","-");
                ZipFile.CreateFromDirectory($"{serverPath}/World/Data", $"{serverPath}/World/Autosaves/{fileName}.zip");
            }
        
            SaveLoadManager.Instance.CreateSaveMetaData();
            SaveLoadManager.Instance.Save();

            if (Directory.GetFiles($"{serverPath}/World/Autosaves").Length < 3) yield break; //Delete old saves - max 3 save files
            DirectoryInfo parentInfo = new($"{serverPath}/World/Autosaves");
            FileInfo[] fileInfos = parentInfo.GetFiles();

            FileInfo oldestFile = fileInfos.OrderBy(file => file.CreationTime).FirstOrDefault();
        
            File.Delete(oldestFile!.FullName);
        }
    }

#endif
    [Serializable]
    public class ServerConfigSettings
    {
        public int MaxPlayers = 50;
        public int MaxPlayersVIP = 75;
        
        public bool Whitelist;
        
        public string Password = "";
        public string GameVersion = "";

        public int SaveInterval = 600;
    }

    [Serializable]
    public class WhiteList
    {
        public List<ulong> Ids;
    }

    [Serializable]
    public class BanList
    {
        public List<ulong> Ids;
    }

    [Serializable]
    public class AdminList
    {
        public List<ulong> Ids;
    }
}

