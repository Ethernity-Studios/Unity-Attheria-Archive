using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        string path = $"{Path}/Data";
        foreach (var dir in Directory.GetDirectories(path))
        {
            GameObject g = Instantiate(Save, MainMenuUIManager.Instance.SavedGames.transform, true);
            g.transform.localScale = Vector3.one;

            SavedGameInstance sg = g.GetComponent<SavedGameInstance>();
            string savePath = $"{path}/{dir}";
            sg.Path = dir;
            sg.SavedWorldInstance = this;
            sg.SaveName = savePath.Split("\\").Last();
            Saves.Add(g);
        }
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
        ConfirmScreenInstance.Instance.OpenDialog(ConfirmScreenDialogs.DeleteWorldTitle, ConfirmScreenDialogs.DeleteWorldDescription);
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
            MainMenuUIManager.Instance.ConfirmScreen.SetActive(false);
        }
        else
        {
            MainMenuUIManager.Instance.ConfirmScreen.SetActive(false);
        }

        ConfirmScreenInstance.Instance.OnButtonClick -= resultDialog;
    }
}