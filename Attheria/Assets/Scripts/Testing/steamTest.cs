using Mirror;
using Mirror.FizzySteam;
using Steamworks;
using UnityEngine;

public class steamTest : MonoBehaviour
{
    void Start()
    {
        if (SteamManager.Initialized)
        {
            SteamUser.GetSteamID();
            SteamAPI.RestartAppIfNecessary(AppId_t.Invalid); 
        }
    }
}
