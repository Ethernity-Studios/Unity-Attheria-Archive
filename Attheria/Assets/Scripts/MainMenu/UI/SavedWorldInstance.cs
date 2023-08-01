using System.Collections.Generic;
using System.IO;
using System.Linq;
using SaveSystem.SaveLoad;
using SaveSystem.WorldSettings;
using TMPro;
using UI;
using UnityEngine;

public class SavedWorldInstance : MonoBehaviour
{
    public string Path;

    public string WorldName;
    public string MapName;

    public WorldSettings WorldSettings;

    [SerializeField] private TMP_Text SaveNameText;
    [SerializeField] private TMP_Text MapNameText;

    [SerializeField] private GameObject Save;
    public List<GameObject> Saves;

    private void Start()
    {
        SaveNameText.text = WorldName;
        MapNameText.text = MapName;
    }

    /// <summary>
    /// Load all saved games
    /// </summary>
    public void LoadSaves()
    {
        MainMenuUIManager.Instance.Saves.SetActive(true);

        reloadSaves();

        //Loads all saves
        string path = $"{Path}/Saves";
        foreach (var dir in Directory.GetDirectories(path))
        {
            GameObject g = Instantiate(Save, MainMenuUIManager.Instance.SavedGames.transform, true);
            g.transform.localScale = Vector3.one;

            SavedGameInstance sg = g.GetComponent<SavedGameInstance>();
            string savePath = $"{path}/{dir}";
            sg.Path = dir;
            sg.SavedWorldInstance = this;
            sg.SaveName = savePath.Split("\\").Last();

            
            string metaPath = $"{dir}/MetaData.dat";
            
            string json = File.ReadAllText(metaPath);
            SaveMetaData data = JsonUtility.FromJson<SaveMetaData>(json);

            sg.Version = data.Version;
            sg.SavePlaytime = data.Playtime;
            sg.SaveDate = data.SaveDate;
            
            Saves.Add(g);
            MainMenuSaveLoadManager.Instance.SaveInstances.Add(g);
        }
        
        //Order saves by save date
        Saves = Saves.OrderBy(x => x.GetComponent<SavedGameInstance>().SaveDate).ToList();
        Saves.Reverse();
        int index = 0;
        foreach (var save in Saves)
        {
            save.transform.SetSiblingIndex(index);
            index++;
        }
    }

    void reloadSaves()
    {
        //Clears all saves
        foreach (var g in Saves)
        {
            Destroy(g);
        }

        Saves.Clear();
    }


    /// <summary>
    /// Load saved world settings
    /// </summary>
    public void ShowWorldSettings()
    {
        MainMenuSaveLoadManager.Instance.LoadedSettings = WorldSettings;
        MainMenuSaveLoadManager.Instance.LoadedWorldPath = Path;
        MainMenuUIManager.Instance.LoadWorldSetting(WorldSettings);
        MainMenuUIManager.Instance.ToggleWorldSettingsMenu();
    }

    /// <summary>
    /// Open confirmation dialog
    /// </summary>
    public void DeleteWorld()
    {
        ConfirmScreenInstance.Instance.OpenDialog(ConfirmScreenDialogs.DeleteWorldTitle, ConfirmScreenDialogs.DeleteWorldDescription, ConfirmScreenDialogs.DeleteWorldPositiveBtn, ConfirmScreenDialogs.DeleteWorldNegativeBtn);
        ConfirmScreenInstance.Instance.OnButtonClick += resultDialog;
    }

    /// <summary>
    /// Delete world save
    /// </summary>
    void resultDialog(int result)
    {
        if ((Result)result == Result.Confirm)
        {
            Directory.Delete(Path, true);
            MainMenuSaveLoadManager.Instance.ReloadSaves();
        }

        MainMenuUIManager.Instance.ConfirmScreen.SetActive(false);
        ConfirmScreenInstance.Instance.OnButtonClick -= resultDialog;
    }
}
