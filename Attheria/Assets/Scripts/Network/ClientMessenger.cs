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
            PlayerSpawner.Instance.OpenSpawner(msg.UnlockedZones);
        }
        else if (msg.OpenCharacterCreator)
        {
            CharacterCreator.Instance.OpenCharacterCreator();
        }
        else
        {
            NetworkClient.AddPlayer(-1);
        }
    }
}
