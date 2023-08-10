using System;
using Mirror;
using Steamworks;
using UnityEngine;

public class Ha : MonoBehaviour
{

    private void Start()
    {
        //Debug.Log("Best scenaaa no cap");

        //Debug.Log("Save : " + GameConfigManager.Instance.SavePath);


    }

    /*private void Start()
    {
        Debug.Log("Starteddddd and giving id" + (ulong)SteamUser.GetSteamID());
        
        Debug.Log("network servr" + NetworkServer.connections);
        Debug.Log("Conn " + connectionToServer);
        Debug.Log("JAAAS " + NetworkServer.connections[connectionToServer.connectionId].SteamId);
        
        NetworkServer.connections[connectionToServer.connectionId].SteamId = (ulong)SteamUser.GetSteamID();
        //OnReady();
    }

    public override void OnStartClient()
    {
        Debug.Log("Starteddddd and giving id" + (ulong)SteamUser.GetSteamID());
        NetworkServer.connections[NetworkConnection.LocalConnectionId].SteamId = (ulong)SteamUser.GetSteamID();
        
    }

    [Command(requiresAuthority = false)]
    void OnReady()
    {
        NetworkServer.localConnection.SteamId = (ulong)SteamUser.GetSteamID();
    }

    public override void OnStartServer()
    {

    }

    public void OnConnectedToServer()
    {
        throw new NotImplementedException();
    }

    public override void OnStopClient()
    {

        base.OnStopClient();
    }*/
}
