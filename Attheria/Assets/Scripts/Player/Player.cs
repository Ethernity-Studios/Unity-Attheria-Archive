using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar] public string Name;
    
    public PlayerData PlayerData;
    [SyncVar(hook = nameof(onCharacterChange))]
    public Character Character;

    void onCharacterChange(Character _, Character newValue)
    {
        
    }
    

    public PlayerData SaveData()
    {
        return new PlayerData();
    }

    [TargetRpc]
    public void LoadData(PlayerData data)
    {
        PlayerData = data;
    }

    /// <summary>
    /// Loads data for all clients
    /// </summary>
    [ClientRpc]
    public void LoadClientData(PlayerData data)
    {
        
    }
}

[Serializable]
public struct PlayerData
{
    #region Server

    public List<int> UnlockedZones;

    public Vector3 Position;
    public Vector3 Rotation; 
        
    public bool Spawned;
    public bool Dead;

    #endregion

    #region Client

    public ulong SteamId;
    public Character Character;

    #endregion
    

}
