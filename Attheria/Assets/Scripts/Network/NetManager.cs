using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class NetManager : NetworkManager
{
    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>($"SpawnablePrefabs").ToList();
        base.OnStartServer();
    }

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>($"SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }

        base.OnStartClient();
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
            Debug.LogError($"Scene change is already in progress for {newSceneName}");
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
}