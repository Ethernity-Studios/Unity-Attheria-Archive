using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;

public class NetManager : NetworkManager
{

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
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (NetworkServer.connections.Count == 1)
        {
            MenuManager.Instance.LoadBtn.SetActive(true);
        }
        
        base.OnServerDisconnect(conn);
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