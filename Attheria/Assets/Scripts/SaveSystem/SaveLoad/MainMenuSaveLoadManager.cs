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

    void instantiateSave(WorldSettings setting, string path)
    {
        GameObject g = Instantiate(World, MainMenuUIManager.Instance.SavedWorlds.transform, true);
        g.transform.localScale = Vector3.one;
        Saves.Add(g);
        var ws = g.GetComponent<SavedWorld>();
        ws.WorldName = path.Split("/").Last().Split("\\")[1];
        ws.MapName = setting.world.MapName;
        ws.Path = path;
        ws.WorldSettings = setting;
    }

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

    TomlTable createTomlTable(WorldSettings settings)
    {
        return new TomlTable()
        {
            ["World"] =
            {
                ["WorldName"] = settings.world.WorldName,
                ["MapName"] = settings.world.MapName,
            },

            ["SomeSettings"] =
            {
                ["TestField"] = settings.someSettings.TestField,
                ["TestIntField"] = settings.someSettings.TestFieldInt
            },
        };
    }

    WorldSettings getTomlSettings(TomlTable table) => TomlLoader.readValue<WorldSettings>(table);
}