using System.Collections.Generic;
using System.IO;
using System.Linq;
using SaveSystem.WorldSettings;
using Tommy;
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

    TomlTable createTomlTable(WorldSettings settings)
    {
        return new TomlTable()
        {
            ["World"] =
            {
                ["WorldName"] = settings.WorldName,
                ["MapName"] = settings.MapName,
            },

            ["SomeSettings"] =
            {
                ["TestField"] = settings.TestField,
                ["TestIntField"] = settings.TestFieldInt
            },
        };
    }

    WorldSettings getTomlSettings(TomlTable table)
    {
        return new WorldSettings()
        {
            MapName = table["World"]["MapName"],
            WorldName = table["World"]["WorldName"],
            TestField = table["SomeSettings"]["TestField"],
            TestFieldInt = table["SomeSettings"]["TestIntField"]
        };
    }
}