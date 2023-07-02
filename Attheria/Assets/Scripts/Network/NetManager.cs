using System.Linq;
using UnityEngine;
using Mirror;

public class NetManager : NetworkManager
{
    
    /// <summary>
    /// Register all spawnable prefabs on server from Resource folder
    /// </summary>
    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>($"SpawnablePrefabs").ToList();
        base.OnStartServer();
    }

    
    /// <summary>
    /// Register all spawnable prefabs on client from Resource folder
    /// </summary>
    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>($"SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }

        base.OnStartClient();
    }
}