using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managers;
using Mirror;

public class PlayerManager : Manager
{
    public static PlayerManager Instance;

    public List<Player> Players;
    public List<PlayerData> PlayersData;

    public override void OnStartServer()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        SaveLoadManager.Instance.DataLoaded += init;
    }

    void init()
    {
    }

    [Server]
    public void OnClientJoin(NetworkConnectionToClient conn)
    {
        StartCoroutine(onClientJoin(conn));
    }

    [Server]
    IEnumerator onClientJoin(NetworkConnectionToClient conn)
    {
        while (SaveLoadManager.Instance == null) yield return null;
        while (!SaveLoadManager.Instance.Loaded) yield return null;

        foreach (PlayerData playerData in PlayersData)
        {
            if (playerData.SteamId == conn.steamId)
            {
                if (playerData.Dead)
                {
                    conn.Send(new PlayerSpawnResponse()
                    {
                        OpenSpawner = true,
                        OpenCharacterCreator = false,
                        UnlockedZones = playerData.UnlockedZones
                    });
                    break;
                }
                else
                {
                    conn.Send(new PlayerSpawnResponse()
                    {
                        OpenSpawner = false,
                        OpenCharacterCreator = false,
                        UnlockedZones = playerData.UnlockedZones
                    });
                    break;
                }
            }
        }

        if (GameConfigManager.Instance.Settings.world.AllZonesUnlocked)
        {
            conn.Send(new PlayerSpawnResponse()
            {
                OpenSpawner = false,
                OpenCharacterCreator = true,
                UnlockedZones = new List<int> { 0 }
            });
        }
        else
        {
            conn.Send(new PlayerSpawnResponse()
            {
                OpenSpawner = false,
                OpenCharacterCreator = true,
                UnlockedZones = new List<int> { -1 }
            });
        }
    }

    [Server]
    public void SaveCharacter(Character character, ulong id)
    {
        Player player = Players.FirstOrDefault(x => x.connectionToClient.steamId == id);
        if (player != null)
        {
            player.Character = character;
        }
        else
        {
        }
    }

    public override Task<object> SaveData() => Task.FromResult<object>(new SavableData()
    {
        PlayersData = PlayersData
    });

    public override Task LoadData(object data)
    {
        var saveData = (SavableData)data;

        PlayersData = saveData.PlayersData;
        return Task.CompletedTask;
    }

    [Serializable]
    private struct SavableData
    {
        public List<PlayerData> PlayersData;
    }

    #region Messages

    public struct PlayerSpawnResponse : NetworkMessage
    {
        public bool OpenSpawner;
        public bool OpenCharacterCreator;
        public List<int> UnlockedZones;
    }

    #endregion
}