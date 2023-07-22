using System.IO;
using TMPro;
using UI;
using UnityEngine;

public class SavedGameInstance : MonoBehaviour
{
    public string Path;

    public string SaveName;
    public string SaveDate;
    public string SavePlaytime;

    [SerializeField] private TMP_Text SaveNameText;
    [SerializeField] private TMP_Text SaveDateText;
    [SerializeField] private TMP_Text SavePlaytimeText;

    [HideInInspector] public SavedWorldInstance SavedWorldInstance;

    private void Start()
    {
        SaveNameText.text = SaveName;
        SaveDateText.text = SaveDate;
        SavePlaytimeText.text = SavePlaytime;
    }

    /// <summary>
    /// Load save file
    /// </summary>
    public void LoadSave()
    {
        MainMenuSaveLoadManager.Instance.LoadedSettings = SavedWorldInstance.WorldSettings;
        MainMenuSaveLoadManager.Instance.LoadedWorldPath = SavedWorldInstance.Path;
        MainMenuSaveLoadManager.Instance.LoadedSavePath = $"{SavedWorldInstance.Path}/Data/{SaveName}";
        
        MainMenuSaveLoadManager.Instance.LoadSave();
    }

    /// <summary>
    /// Check how many save files there are
    /// Open confirmation dialog
    /// </summary>
    public void DeleteSave()
    {
        if (SavedWorldInstance.Saves.Count == 1)
        {
            ConfirmScreenInstance.Instance.OpenDialog(ConfirmScreenDialogs.DeleteSaveWorldTitle, ConfirmScreenDialogs.DeleteSaveWorldDescription);
            ConfirmScreenInstance.Instance.OnButtonClick += resultDialogWorld;
        }
        else if (SavedWorldInstance.Saves.Count > 1)
        {
            ConfirmScreenInstance.Instance.OpenDialog(ConfirmScreenDialogs.DeleteSaveTitle, ConfirmScreenDialogs.DeleteSaveDescription);
            ConfirmScreenInstance.Instance.OnButtonClick += resultDialogSave;
        }
    }

    /// <summary>
    /// Delete only save
    /// </summary>
    void resultDialogSave(int result)
    {
        if ((Result)result == Result.Confirm)
        {
            Directory.Delete(Path, true);
            SavedWorldInstance.Saves.Remove(gameObject);
            Destroy(gameObject);
            MainMenuUIManager.Instance.ConfirmScreen.SetActive(false);
        }
        else
        {
            MainMenuUIManager.Instance.ConfirmScreen.SetActive(false);
        }

        ConfirmScreenInstance.Instance.OnButtonClick -= resultDialogSave;
    }

    /// <summary>
    /// Delete save and world
    /// </summary>
    void resultDialogWorld(int result)
    {
        if ((Result)result == Result.Confirm)
        {
            Directory.Delete(SavedWorldInstance.Path, true);
            MainMenuSaveLoadManager.Instance.ReloadSaves();
            Destroy(gameObject);
            MainMenuUIManager.Instance.ConfirmScreen.SetActive(false);
        }
        else
        {
            MainMenuUIManager.Instance.ConfirmScreen.SetActive(false);
        }

        ConfirmScreenInstance.Instance.OnButtonClick -= resultDialogWorld;
    }
}