using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mirror;
using SaveSystem.SaveLoad;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : NetworkBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject Menu;
    [Header("Main Menu")] 
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SaveBtn;
    [SerializeField] public GameObject LoadBtn;
    [Header("Save Menu")] 
    [SerializeField] private GameObject SaveMenu;
    [SerializeField] private Transform SavesHolder;
    [SerializeField] private GameObject SaveInstance;
    public string SelectedSaveName;
    public string SelectedSavePath;
    public TMP_InputField SaveNameInp;
    [Header("Load Menu")]
    [SerializeField] private GameObject LoadMenu;
    [SerializeField] private Transform LoadSavesHolder;
    [SerializeField] private GameObject LoadSaveInstance;
    [SerializeField] private GameObject LoadingScreenCanvas;
    [SerializeField] private TMP_Text LoadingScreenWorldTitle;
    [Header("Saves")]
    public List<GameObject> Saves;
    public List<string> SaveNames;
    [Header("Settings Menu")]
    [SerializeField] private GameObject SettingsMenu;
    [Header("Reusable Components")]
    public GameObject ConfirmScreen;
    [SerializeField] private GameObject BackButton;

    private PlayerInput input;
    private bool opened = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        ConfirmScreen.SetActive(true);
        ConfirmScreen.SetActive(false);
    }

    private void Start()
    {
        if (isServer)
        {
            SaveLoadManager.Instance.DataLoaded += init;
            SaveBtn.SetActive(true);
            LoadBtn.SetActive(true);
            return;
        }
        init();
    }

    void init()
    {
        input = new();
        
        input.Menu.Enable();
        input.Menu.Toggle.performed += toggleMenu;
    }

    private void OnDisable()
    {
        input?.Menu.Disable();
    }

    void toggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        opened = !opened; 
        MainMenu.SetActive(opened);
        Menu.SetActive(opened);

        if(opened) Back();
        if(!opened) closeAllMenus();

        if (!isServer) return;
        //Pause game if host mode && if pause is possible
        TimeManager.Paused = isServer switch
        {
            true when TimeManager.Instance.CanBePaused && opened => true,
            true when !opened => false,
            _ => TimeManager.Paused
        };
    }

    public void Resume()
    {
        MainMenu.SetActive(false);
        Menu.SetActive(false);
        opened = false;
    }

    void openMainMenu()
    {
        MainMenu.SetActive(true);
    }

    public void OpenSaveMenu()
    {
        closeAllMenus();
        SaveMenu.SetActive(true);
        BackButton.SetActive(true);
        
        loadSaves();
    }

    public void Save()
    {
        if (SaveNameInp.text == string.Empty) return;
        if (SaveNames.Any(name => name == SaveNameInp.text))
        {
            ConfirmScreenInstance.Instance.OpenDialog(ConfirmScreenDialogs.OverrideSaveTitle, ConfirmScreenDialogs.OverrideSaveDescription, ConfirmScreenDialogs.OverrideSavePositiveBtn, ConfirmScreenDialogs.OverrideSaveNegativeBtn);
            ConfirmScreenInstance.Instance.OnButtonClick += saveResultDialog;
        }
        else
        {
            saveResultDialog(1); 
        }
    }

    public void OpenLoadMenu()
    {
        closeAllMenus();
        LoadMenu.SetActive(true);
        BackButton.SetActive(true);
        
        loadLoadSaves();
    }

    public void OpenSettingsMenu()
    {
        closeAllMenus();
        SettingsMenu.SetActive(true);
        BackButton.SetActive(true);
    }

    void closeAllMenus()
    {
        MainMenu.SetActive(false);
        SaveMenu.SetActive(false);
        LoadMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        
        BackButton.SetActive(false);

        foreach (var save in Saves)
        {
            Destroy(save);
        }
        Saves.Clear();
    }

    public void Exit()
    {
        ConfirmScreenInstance.Instance.OpenDialog(ConfirmScreenDialogs.ExitWorldTitle, ConfirmScreenDialogs.ExitWorldDescription, ConfirmScreenDialogs.ExitWorldPositiveBtn, ConfirmScreenDialogs.ExitWorldNegativeBtn);
        ConfirmScreenInstance.Instance.OnButtonClick += exitResultDialog;
    }

    public void Back()
    {
        MainMenu.SetActive(true);
        
        SaveMenu.SetActive(false);
        LoadMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        BackButton.SetActive(false);
    }

    public void SetSaveName(string name) => SelectedSaveName = name;

    public void RemoveSave(string path)
    {
        Directory.Delete(path, true);
        loadSaves();
    }

    void loadSaves()
    {
        reloadSaves();
        
        foreach (var path in Directory.GetDirectories($"{GameConfigManager.Instance.WorldPath}/Saves"))
        {
            var s = Instantiate(SaveInstance, SavesHolder, true);
            s.transform.localScale = Vector3.one;
            SaveInstance instance = s.GetComponent<SaveInstance>();
            string name = path.Split("/").Last().Split("\\")[1];

            string savePath = $"{path}/MetaData.dat";
            
            if (!File.Exists(savePath)) continue;
            string json = File.ReadAllText(savePath);
            SaveMetaData data = JsonUtility.FromJson<SaveMetaData>(json);

            instance.Date = data.SaveDate;
            instance.Version = data.Version;
            instance.PlayTime = data.Playtime;
            instance.Name = name;
            instance.Path = path;
            instance.Init();
            
            Saves.Add(s);
            SaveNames.Add(name);
        }
        //Order saves by save date
        Saves = Saves.OrderBy(x => x.GetComponent<SaveInstance>().Date).ToList();
        Saves.Reverse();
        int index = 1;
        foreach (var save in Saves)
        {
            save.transform.SetSiblingIndex(index);
            index++;
        }
    }

    void loadLoadSaves()
    {
        reloadSaves();
        
        foreach (var path in Directory.GetDirectories($"{GameConfigManager.Instance.WorldPath}/Saves"))
        {
            var s = Instantiate(LoadSaveInstance, LoadSavesHolder, true);
            s.transform.localScale = Vector3.one;
            LoadInstance instance = s.GetComponent<LoadInstance>();
            string name = path.Split("/").Last().Split("\\")[1];

            string savePath = $"{path}/MetaData.dat";
            
            if (!File.Exists(savePath)) continue;
            string json = File.ReadAllText(savePath);
            SaveMetaData data = JsonUtility.FromJson<SaveMetaData>(json);

            instance.Date = data.SaveDate;
            instance.Version = data.Version;
            instance.PlayTime = data.Playtime;
            instance.Name = name;
            instance.Path = path;
            instance.Init();
            
            Saves.Add(s);
            SaveNames.Add(name);
        }
        //Order saves by save date
        Saves = Saves.OrderBy(x => x.GetComponent<LoadInstance>().Date).ToList();
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
        foreach (var g in Saves)
        {
            Destroy(g);
        }
        
        Saves.Clear();
        SaveNames.Clear();
    }

    public void LoadSave(string path)
    {
        GameConfigManager.Instance.SavePath = path;

        LoadingScreenCanvas.SetActive(true);
        LoadingScreenWorldTitle.text = GameConfigManager.Instance.Settings.world.WorldName;

        NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
        
    }

    #region resultDialogs
    void exitResultDialog(int result)
    {
        if ((Result)result == Result.Confirm)
        {
            ConfirmScreen.SetActive(false);
            ConfirmScreenInstance.Instance.OnButtonClick -= exitResultDialog;
            if (isServer)
            {
                NetworkManager.singleton.StopHost();
            }
            else if (isClient)
            {
                NetworkManager.singleton.StopClient();
            }
        }
        else
        {
            ConfirmScreen.SetActive(false);
            ConfirmScreenInstance.Instance.OnButtonClick -= exitResultDialog;  
        }
    }
    
    void saveResultDialog(int result)
    {
        if ((Result)result == Result.Confirm)
        {
            closeAllMenus();
           openMainMenu();

           Directory.CreateDirectory($"{GameConfigManager.Instance.WorldPath}/Saves");
           string newSavePath = $"{GameConfigManager.Instance.WorldPath}/Saves/{SelectedSaveName}";
           Directory.CreateDirectory(newSavePath);
           GameConfigManager.Instance.SavePath = newSavePath;
           SaveLoadManager.Instance.CreateSaveMetaData();
           
           SaveLoadManager.Instance.Save();
        }
        ConfirmScreen.SetActive(false);
        ConfirmScreenInstance.Instance.OnButtonClick -= saveResultDialog;
    }

    #endregion
    

    #region Debug Menu

    public void Respawn()
    {
        //TODO
    }

    #endregion
}