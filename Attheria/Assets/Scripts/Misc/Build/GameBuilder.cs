using System;
using System.IO;
using CSharpDiscordWebhook.NET.Discord;
using UnityEditor;
using UnityEngine;

public static class GameBuilder
{
#if UNITY_EDITOR
    
    private static DiscordWebhook hook;
    
    [MenuItem("Build/Build All")]
    public static void BuildAll()
    {
        BuildWindowsServer();
        //BuildLinuxServer(); //Need to install
        BuildMonoWindowsClient();
        BuildMonoLinuxClient();
        //BuildIl2cppWindowsClient();
        //BuildIl2cppLinuxClient();
    }

    [MenuItem("Build/Build Server (Windows)")]
    public static void BuildWindowsServer()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Maps/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Server/Server.exe",
            //locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName}/Server.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Server
        };

        Console.WriteLine("Building Server (Windows)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Server (Windows).");

        sendDiscordBuildMessage("Windows server");
    }

    [MenuItem("Build/Build Server (Linux)")]
    public static void BuildLinuxServer()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Maps/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Server_Linux/Server.x86_64",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Server
        };

        Console.WriteLine("Building Server (Linux)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Server (Linux).");
        
        sendDiscordBuildMessage("Linux server");
    }


    [MenuItem("Build/Build Client (Windows Mono)")]
    public static void BuildMonoWindowsClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Maps/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client/Mono/Attheria.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Windows Mono)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Windows Mono).");
        
        sendDiscordBuildMessage("Windows Mono client");
    }

    [MenuItem("Build/Build Client (Linux Mono)")]
    public static void BuildMonoLinuxClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Maps/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client_Linux/Mono/Attheria",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Linux Mono)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Linux Mono).");
        
        sendDiscordBuildMessage("Linux Mono client");
    }

    [MenuItem("Build/Build Client (Windows Il2cpp)")]
    public static void BuildIl2cppWindowsClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Maps/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client/Il2cpp/Attheria.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Windows Il2cpp)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Windows Il2cpp).");
        
        sendDiscordBuildMessage("Windows Il2cpp client");
    }

    [MenuItem("Build/Build Client (Linux Il2cpp)")]
    public static void BuildIl2cppLinuxClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Maps/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client_Linux/Il2cpp/Attheria",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Linux Il2cpp)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Linux Il2cpp).");
        
        sendDiscordBuildMessage("Linux Il2cpp client");
    }
    
    static void sendDiscordBuildMessage(string build)
    {
        hook = new DiscordWebhook
        {
            Uri = new("https://discord.com/api/webhooks/1150840793028898828/8upYbIScgnierHaDVpqv4vJs1-Mii_06rRIM1xRgS22weH0iTWwVMdjqyItGZc8AiQxN")
        };

        DiscordMessage message = new DiscordMessage
        {
            Content = $@"New **{build}** build | game version **{GameManager.GameVersion}**",
            TTS = true, //read message to everyone on the channel
            Username = "Bot the Builder",
            AvatarUrl = new("https://cdn-icons-png.flaticon.com/512/4712/4712126.png"),
        };

        Send(message);
    }

    static async void Send(DiscordMessage message)
    {
        await hook.SendAsync(message);
    }
#endif
}