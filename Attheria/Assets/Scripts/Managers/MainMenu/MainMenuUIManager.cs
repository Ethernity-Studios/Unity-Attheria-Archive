using System;
using SaveSystem.WorldSettings;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MainMenu
{
    public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu")] 
    public Button MultiplayerBtn;
    public Button SingleplayerBtn;
    public Button SettingsBtn;
    public Button ExitBtn;

    public TMP_Text GameVersion;

    [Header("Server messages")] 
    public GameObject ResponseMessageInstance;
    public GameObject ServerPasswordInstance;
    public ResponseMessageInstance ResponseMessage;
    public ServerPasswordInstance ServerPassword;

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

    [Header("Saves")] 
    public GameObject Saves;
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
    [Header("Loading screen")] 
    public GameObject LoadingScreenCanvas;
    public TMP_Text LoadingScreenWorldTitle;
    public static MainMenuUIManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        ConfirmScreen.SetActive(true);
        ConfirmScreen.SetActive(false);
    }

    private void Start()
    {
        GameVersion.text = GameManager.Instance.GameVersion;
    }

    /// <summary>
/// Open main screen
/// </summary>
    public void OpenMainMenuScreen()
    {
        closeScreens();
        BackBtn.SetActive(false);
        MainMenuScreen.SetActive(true);
    }
/// <summary>
/// Opens multiplayer screen
/// </summary>
    public void OpenMultiplayerScreen()
    {
        closeScreens();
        BackBtn.SetActive(true);
        MultiplayerScreen.SetActive(true);
    }
/// <summary>
/// Opens singleplayer screen
/// </summary>
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
/// <summary>
/// Opens game settings screen
/// </summary>
    public void OpenSettingsScreen()
    {
        closeScreens();
        BackBtn.SetActive(true);
        SettingsScreen.SetActive(true);
    }
/// <summary>
/// Closes all screens
/// </summary>
    private void closeScreens()
    {
        MainMenuScreen.SetActive(false);
        MultiplayerScreen.SetActive(false);
        SingleplayerScreen.SetActive(false);
        SettingsScreen.SetActive(false);

        settingsMenuOpened = false;
        WorldSettingsScreen.SetActive(false);
    }
/// <summary>
/// Back button callback
/// </summary>
    public void BackToMainMenu()
    {
        OpenMainMenuScreen();

        foreach (var save in MainMenuSaveLoadManager.Instance.SaveInstances)
        {
            Destroy(save);
        }
        MainMenuSaveLoadManager.Instance.SaveInstances.Clear();
    }
/// <summary>
/// Closes application
/// </summary>
    public void CloseApplication() => Application.Quit();
   
/// <summary>
/// Toggles world settings 
/// </summary>
    public void ToggleWorldSettingsMenu()
    {
        settingsMenuOpened = !settingsMenuOpened;
        WorldSettingsScreen.SetActive(settingsMenuOpened);
    }
/// <summary>
/// Loads world settings from param settings
/// </summary>
/// <param name="settings"></param>
    public void LoadWorldSetting(WorldSettings settings)
    {
        TestField.text = settings.someSettings.TestField;
        TestIntField.text = settings.someSettings.TestFieldInt.ToString();
    }
/// <summary>
/// Loads default world settings
/// </summary>
    void loadDefaultWorldSettings()
    {
        TestField.text = DefaultWorldSettings.TestField;
        TestIntField.text = DefaultWorldSettings.TestFieldInt.ToString();
    }
/// <summary>
/// Closes world settings menu
/// </summary>
    public void CloseWorldSettingsMenu() => ToggleWorldSettingsMenu();
/// <summary>
/// saves and closes world settings
/// </summary>
    public void SaveWorldSettingsMenu()
    {
        WorldSettings settings = MainMenuSaveLoadManager.Instance.LoadedSettings;
        if (settings != null)
        {
            settings.someSettings.TestField = TestField.text;
            settings.someSettings.TestFieldInt = int.Parse(TestIntField.text);
            
            MainMenuSaveLoadManager.Instance.OverrideWorldSettings(settings, MainMenuSaveLoadManager.Instance.LoadedWorldPath);
        }
        ToggleWorldSettingsMenu();
    }
/// <summary>
/// Create world callback
/// </summary>
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

/// <summary>
/// Shows loading screen
/// </summary>
    public void ShowLoadingScreen()
    {
        LoadingScreenCanvas.SetActive(true);
        LoadingScreenWorldTitle.text = GameConfigManager.Instance.Settings.world.WorldName;
    }
}
}
