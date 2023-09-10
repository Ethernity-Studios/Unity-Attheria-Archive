using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Server;
using UnityEngine;
using Mirror;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NetManager : NetworkManager
{
    public List<Spawnpoint> Spawnpoints;
    
    #region Server

    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>($"SpawnablePrefabs").ToList();
        base.OnStartServer();
    }

    public override void ServerChangeScene(string newSceneName)
    {
        if (string.IsNullOrWhiteSpace(newSceneName))
        {
            Debug.LogError("ServerChangeScene empty scene name");
            return;
        }

        if (NetworkServer.isLoadingScene && newSceneName == networkSceneName)
        {
            //Debug.LogError($"Scene change is already in progress for {newSceneName}"); //Was throwing some errors when i reloaded game scene
            return;
        }

        // Debug.Log($"ServerChangeScene {newSceneName}");
        NetworkServer.SetAllClientsNotReady();
        networkSceneName = newSceneName;

        // Let server prepare for scene change
        OnServerChangeScene(newSceneName);

        // set server flag to stop processing messages while changing scenes
        // it will be re-enabled in FinishLoadScene.
        NetworkServer.isLoadingScene = true;

        loadingSceneAsync = SceneManager.LoadSceneAsync(newSceneName);

        //It threw error when exiting playmode :melting_face: so keep the if there thx
        if (SceneManager.GetActiveScene().buildIndex == 0) loadingSceneAsync.allowSceneActivation = false;
#if UNITY_SERVER
        loadingSceneAsync.allowSceneActivation = true;
#endif

        // ServerChangeScene can be called when stopping the server
        // when this happens the server is not active so does not need to tell clients about the change
        if (NetworkServer.active)
        {
            // notify all clients about the new scene
            NetworkServer.SendToAll(new SceneMessage
            {
                sceneName = newSceneName
            });
        }

        startPositionIndex = 0;
        startPositions.Clear();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        MenuManager.Instance.LoadBtn.SetActive(false);

        if (NetworkServer.connections.Count > 1)
        {
            TimeManager.Instance.CanBePaused = false;
            TimeManager.Paused = false;
        }
        else
        {
            TimeManager.Instance.CanBePaused = true;
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (NetworkServer.connections.Count == 1)
        {
            MenuManager.Instance.LoadBtn.SetActive(true);
            TimeManager.Instance.CanBePaused = true;
        }

        base.OnServerDisconnect(conn);
    }

    public List<Transform> Positions;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn, int zoneId)
    {
        Vector3 spawnPos = Vector3.zero;
        Vector3 spawnRot = Vector3.zero;
        PlayerData playerData = PlayerManager.Instance.PlayersData.FirstOrDefault(x => x.SteamId == conn.steamId);
        if (zoneId == -1)
        {
            if (playerData.SteamId == 0) return;
            if (playerData.Dead) return;
            if (playerData.Spawned) return;

            spawnPos = playerData.Position;
            spawnRot = playerData.Rotation;
        }
        else
        {
            var spawnpoints = PlayerSpawner.Instance.Zones.FirstOrDefault(x => x.ZoneData.Id == zoneId).ZoneData.SpawnPoints;
            
            int rnd = Random.Range(0, spawnpoints.Count);
            if (GameConfigManager.Instance.Settings.world.AllZonesUnlocked)
            {
                spawnPos = spawnpoints[rnd].position;
                spawnRot = spawnpoints[rnd].rotation.eulerAngles;
            }
            else
            {
                if (playerData.UnlockedZones == null) //No player data
                {
                    spawnpoints = PlayerSpawner.Instance.Zones.FirstOrDefault(x => x.ZoneData.Id == 0).ZoneData.SpawnPoints;
                    spawnPos = spawnpoints[rnd].position;
                    spawnRot = spawnpoints[rnd].rotation.eulerAngles;
                }
                else
                {
                    if (!playerData.UnlockedZones.Contains(zoneId)) return;
                    spawnPos = spawnpoints[rnd].position;
                    spawnRot = spawnpoints[rnd].rotation.eulerAngles;
                }
            }
        }
        
        GameObject instance = Instantiate(playerPrefab, spawnPos,  Quaternion.Euler(spawnRot));
        
        instance.name = $"Player | {conn.steamId}";
        Player player = instance.GetComponent<Player>();
        PlayerManager.Instance.Players.Add(player);
        NetworkServer.AddPlayerForConnection(conn, instance);

        PlayerManager.Instance.LoadPlayerData(conn.steamId, player);
    }



    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        PlayerManager.Instance.OnClientJoin(conn);

        //base.OnServerReady(conn);
    }

    public override void OnStopServer()
    {
        NetworkServer.SendToAll(new DisconnectMessage()
        {
            ErrorCode = -1,
            Message = "Lost connection to server"
        });
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {

        var spawnablePrefabs = Resources.LoadAll<GameObject>($"SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }

        NetworkClient.RegisterHandler<DisconnectMessage>(OnDisconnect);
        base.OnStartClient();
    }

    public void OnDisconnect(DisconnectMessage msg)
    {
        Debug.Log("My steam id is: " + msg.Message);
    }

    #endregion

    #region Messages

    public struct DisconnectMessage : NetworkMessage
    {
        public int ErrorCode;
        public string Message;
    }

    #endregion
}