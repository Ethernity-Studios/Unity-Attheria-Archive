using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [Header("UI Components")] [SerializeField]
    private GameObject MenuGO;

    private PlayerInput input;

    private bool opened = false;
    
    private void Start()
    {
        SaveLoadManager.Instance.DataLoaded += init;
    }

    void init()
    {
        input = new();
        
        input.Menu.Enable();
        input.Menu.Toggle.performed += toggleMenu;
    }

    private void OnDisable()
    {
        input.Menu.Disable();
    }

    void toggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        opened = !opened; 
        MenuGO.SetActive(opened);
    }

    public void Resume()
    {
        MenuGO.SetActive(false);
        opened = false;
    }

    public void Save()
    {
        
    }

    public void Load()
    {
        
    }

    public void Settings()
    {
        
    }

    public void Exit()
    {
        
    }

    #region Debug Menu

    public void Respawn()
    {
        //TODO
    }

    #endregion
}