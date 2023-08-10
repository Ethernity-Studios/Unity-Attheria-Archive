using System.Collections.Generic;
using SaveSystem.WorldSettings;
using UnityEngine;

public class GameConfigManager : MonoBehaviour
{
    public static GameConfigManager Instance { get; set; }

    public WorldSettings Settings;
    public ServerManager.ServerConfigSettings ServerSettings = null;
    public string WorldPath;
    public string SavePath;

    public List<ulong> WhitelistedIds; 
    public List<ulong> BannedIds; 
    public List<ulong> AdminIds;
    public List<ulong> VIPIds; 

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }
}
