using SaveSystem.WorldSettings;
using UnityEngine;

public class GameConfigManager : MonoBehaviour
{
    public static GameConfigManager Instance { get; set; }

    public WorldSettings Settings;
    public string SavePath;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
