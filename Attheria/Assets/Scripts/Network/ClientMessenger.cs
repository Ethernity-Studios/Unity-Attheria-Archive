using Mirror;
using UnityEngine;

public class ClientMessenger : MonoBehaviour
{
    private void OnEnable()
    {
        NetworkClient.RegisterHandler<PlayerManager.PlayerSpawnResponse>(onSpawnResponse, false);
    }

    private void OnDisable()
    {
        NetworkClient.UnregisterHandler<PlayerManager.PlayerSpawnResponse>();
    }

    void onSpawnResponse(PlayerManager.PlayerSpawnResponse msg)
    {
        if (msg.OpenSpawner)
        {
            Debug.Log("1");

            PlayerSpawner.Instance.OpenSpawner(msg.UnlockedZones);
        }
        else if (msg.OpenCharacterCreator)
        {
            Debug.Log("2");

            CharacterCreator.Instance.OpenCharacterCreator();
        }
        else
        {
            Debug.Log("3");
            NetworkClient.AddPlayer(-1);
        }
    }
}
