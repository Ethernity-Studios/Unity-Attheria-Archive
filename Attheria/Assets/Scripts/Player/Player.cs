using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public PlayerData PlayerData;

    [Header("Server data")] 
    public List<int> UnlockedZones;
    public bool Spawned;
    public bool Dead;
    
    [Header("Client data")]
    [SyncVar] public string Name;
    [SyncVar] public ulong SteamId;
    [SyncVar] public Character Character;

    [Server]
    public PlayerData SaveData()
    {
        return new PlayerData()
        {
            #region Server

            UnlockedZones = UnlockedZones,
            Position = transform.position,
            Rotation = transform.rotation.eulerAngles,
            Spawned = Spawned,
            Dead = Dead,

            #endregion


            #region Client

            SteamId = SteamId,
            Character = Character

            #endregion
        };
    }

    [TargetRpc]
    public void LoadData(PlayerData data)
    {
        Debug.Log("Loaded data for local client  + " + data.SteamId);

        PlayerData = data;
        UnlockedZones = data.UnlockedZones;
        Spawned = data.Spawned;
        Dead = data.Dead;
    }

    /// <summary>
    /// Loads data for all clients
    /// </summary>
    [ClientRpc]
    public void LoadClientData(PlayerData data)
    {
        Debug.Log("Loaded data for all clients  + " + data.SteamId);
        
        Name = data.Character.Name;
        SteamId = data.SteamId;
        Character = data.Character;
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
