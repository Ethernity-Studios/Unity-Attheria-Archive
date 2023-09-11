using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMenu : NetworkBehaviour
{
    public static DebugMenu Instance;
    
    private GlobalInput input;
    [HideInInspector] public bool Opened;

    [SerializeField] private GameObject DebugMenuGO;
    [Header("Client")]
    [HideInInspector] public Vector3 PlayerCoords;
    [SerializeField] private TMP_Text GameBuildVersionText;
    [SerializeField] private TMP_Text PlayerCoordsText;
    [SerializeField] private TMP_Text NetworkLatencyText;
    [Header("Network Client")]
    [SerializeField] private TMP_Text ClientPacketsText;
    [SerializeField] private TMP_Text ClientBytesText;
    [Header("Server")]
    [SerializeField] private TMP_Text ServerUptimeText;
    [SerializeField] private TMP_Text ServerConnectionsText;
    [Header("Network Server")]
    [SerializeField] private TMP_Text ServerPacketsText;
    [SerializeField] private TMP_Text ServerBytesText;


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        input = new();
        
        input.DebugMenu.Enable();
        input.DebugMenu.Toggle.performed += toggleMenu;
    }

    void init()
    {
        GameBuildVersionText.text = GameManager.GameVersion;
    }

    private void Update()
    {
        if (!Opened) return;
        PlayerCoordsText.text = $"X: {PlayerCoords.x} Y: {PlayerCoords.y} Z: {PlayerCoords.z}";
        NetworkLatencyText.text = NetworkTime.rtt.ToString();

        ClientPacketsText.text = $"{NetworkStatistics.clientReceivedPacketsPerSecond}\\/ {NetworkStatistics.clientSentPacketsPerSecond}/\\";
        ClientBytesText.text = $"{NetworkStatistics.clientReceivedBytesPerSecond}\\/ {NetworkStatistics.clientSentBytesPerSecond}/\\";
        
        if (!isServer) return;
        ServerUptimeText.text = GolbyUtils.TimeUtil.FormatTime(NetworkTime.time);
        ServerConnectionsText.text = NetworkServer.connections.Count.ToString();
        
        ServerPacketsText.text = $"{NetworkStatistics.serverReceivedPacketsPerSecond}\\/ {NetworkStatistics.serverSentPacketsPerSecond}/\\";
        ServerBytesText.text = $"{NetworkStatistics.serverReceivedBytesPerSecond}\\/ {NetworkStatistics.serverSentBytesPerSecond}/\\";
    }

    private void OnDisable()
    {
        input?.DebugMenu.Disable();
    }

    void toggleMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Opened = !Opened;

        switch (Opened)
        {
            case true:
                DebugMenuGO.SetActive(true);
                break;
            case false:
                DebugMenuGO.SetActive(false);
                break;
        }
    }
    
}
