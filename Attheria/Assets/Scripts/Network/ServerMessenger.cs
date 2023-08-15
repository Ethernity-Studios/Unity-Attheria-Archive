using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

public class ServerMessenger : NetworkBehaviour
{
    public override void OnStartServer()
    {
        NetworkServer.RegisterHandler<CharacterCreator.CharacterRequest>(CharacterCreationRequest);
    }

    public override void OnStopServer()
    {
        NetworkServer.UnregisterHandler<CharacterCreator.CharacterRequest>();
    }

    void CharacterCreationRequest(NetworkConnectionToClient conn, CharacterCreator.CharacterRequest msg)
    {
        if (msg.Character.Name == string.Empty) return;
        
        PlayerManager.Instance.SaveCharacter(msg.Character, conn.steamId);
        PlayerData playerData = PlayerManager.Instance.PlayersData.FirstOrDefault(x => x.SteamId == conn.steamId);
        List<int> unlockedZones = new(){0};
        if (playerData.UnlockedZones != null) unlockedZones = playerData.UnlockedZones;
        
        if (GameConfigManager.Instance.Settings.world.AllZonesUnlocked)
        {
            conn.Send(new CharacterCreator.CharacterResponse
            {
                UnlockedZones = new List<int>(){-1}
            });
        }
        else
        {
            conn.Send(new CharacterCreator.CharacterResponse
            {
                UnlockedZones = unlockedZones
            });
        }
        
    }
}
