using System.Linq;
using UnityEngine;
using Mirror;

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
}