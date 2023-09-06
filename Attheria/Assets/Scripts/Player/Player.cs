using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public PlayerData PlayerData;
    
    public string Name;
    //[SyncVar(hook = nameof(onCharacterChange))]
    public Character Character;


    void onCharacterChange(Character _, Character newValue)
    {
        
    }
}

[Serializable]
public struct PlayerData
{
    public ulong SteamId;
    public bool Spawned;
    public bool Dead;
    public Character Character;

    public List<int> UnlockedZones;

    public Vector3 Position;
    public Vector3 Rotation;
}
