using System;
using SaveSystem.WorldSettings;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu")] 
    public Button MultiplayerBtn;
    public Button SingleplayerBtn;
    public Button SettingsBtn;
    public Button ExitBtn;

    [Header("Reusable Components")]
    public GameObject BackBtn;
    public GameObject ConfirmScreen;
    
    [Header("Screens")] 
    public GameObject MainMenuScreen;
    public GameObject MultiplayerScreen;
    public GameObject SingleplayerScreen;
    public GameObject SettingsScreen;

    [Header("Saved Worlds")] 
    public GameObject SavedWorlds;

    [Header("Saves")] public GameObject Saves;
    public GameObject SavedGames;

    [Header("New Game")] 
    public Button StartGameBtn;

    public TMP_InputField WorldNameInput;
    public Button NewWorldSettingsButton;

    [Header("World Settings")] 
    public GameObject WorldSettingsScreen;
    public Button SettingsSaveBtn;

    [Header("Settings fields")] 
    public TMP_InputField TestField;
    public TMP_Text TestFieldDefault;
    [Space(10)]
    public TMP_InputField TestIntField;
    public TMP_Text TestIntFieldDefault;
    [Space(10)]
    private bool settingsMenuOpened = false;
    private bool firstSingleplayerOpen = true;
    
    public static MainMenuUIManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        ConfirmScreen.SetActive(true);
        ConfirmScreen.SetActive(false);
    }

    public void OpenMainMenuScreen()
    {
        closeScreens();
        BackBtn.SetActive(false);
        MainMenuScreen.SetActive(true);
    }

    public void OpenMultiplayerScreen()
    {
        closeScreens();
        BackBtn.SetActive(true);
        MultiplayerScreen.SetActive(true);
    }

    public void OpenSingleplayerScreen()
    {
        if (firstSingleplayerOpen)
        {
            loadDefaultWorldSettings();
            MainMenuSaveLoadManager.Instance.LoadSaves();
            firstSingleplayerOpen = false;
        }
        else
        {
            MainMenuSaveLoadManager.Instance.ReloadSaves();
        }

        closeScreens();
        BackBtn.SetActive(true);
        SingleplayerScreen.SetActive(true);
    }

    public void OpenSettingsScreen()
    {
        closeScreens();
        BackBtn.SetActive(true);
        SettingsScreen.SetActive(true);
    }

    private void closeScreens()
    {
        MainMenuScreen.SetActive(false);
        MultiplayerScreen.SetActive(false);
        SingleplayerScreen.SetActive(false);
        SettingsScreen.SetActive(false);

        settingsMenuOpened = false;
        WorldSettingsScreen.SetActive(false);
    }

    public void BackToMainMenu()
    {
        OpenMainMenuScreen();
    }

    public void CloseApplication() => Application.Quit();
    
    public void ToggleWorldSettingsMenu()
    {
        settingsMenuOpened = !settingsMenuOpened;
        WorldSettingsScreen.SetActive(settingsMenuOpened);
    }

    public void LoadWorldSetting(WorldSettings settings)
    {
        TestField.text = settings.someSettings.TestField;
        TestIntField.text = settings.someSettings.TestFieldInt.ToString();
    }

    void loadDefaultWorldSettings()
    {
        TestField.text = DefaultWorldSettings.TestField;
        TestIntField.text = DefaultWorldSettings.TestFieldInt.ToString();
    }

    public void CloseWorldSettingsMenu() => ToggleWorldSettingsMenu();

    public void SaveWorldSettingsMenu()
    {
        WorldSettings settings = MainMenuSaveLoadManager.Instance.LoadedSettings;
        if (settings != null)
        {
            settings.someSettings.TestField = TestField.text;
            settings.someSettings.TestFieldInt = int.Parse(TestIntField.text);
            
            MainMenuSaveLoadManager.Instance.OverrideWorldSettings(settings, MainMenuSaveLoadManager.Instance.LoadedWorldPath);
        }
        MainMenuSaveLoadManager.Instance.LoadedSettings = null;
        ToggleWorldSettingsMenu();
    }

    public void CreateSave()
    {
        WorldSettings settings = new()
        {
            world = new()
            {
                WorldName = WorldNameInput.text == string.Empty ? DefaultWorldSettings.WorldName : WorldNameInput.text,
                MapName = DefaultWorldSettings.MapName
            },
            someSettings = new()
            {
                TestField = TestField.text ?? DefaultWorldSettings.TestField,
                TestFieldInt = int.Parse(TestIntField.text),
            }
        };
        MainMenuSaveLoadManager.Instance.CreateSave(settings);
    }
}