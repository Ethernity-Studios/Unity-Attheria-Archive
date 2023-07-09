using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class GameBuilder
{
#if UNITY_EDITOR
    [MenuItem("Build/Build All")]
    public static void BuildAll()
    {
        BuildWindowsServer();
        //BuildLinuxServer(); //Need to install
        BuildMonoWindowsClient();
        BuildMonoLinuxClient();
        BuildIl2cppWindowsClient();
        BuildIl2cppLinuxClient();
    }

    [MenuItem("Build/Build Server (Windows)")]
    public static void BuildWindowsServer()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Server/Server.exe",
            //locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName}/Server.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Server
        };

        Console.WriteLine("Building Server (Windows)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Server (Windows).");
    }

    [MenuItem("Build/Build Server (Linux)")]
    public static void BuildLinuxServer()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Server_Linux/Server.x86_64",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Server
        };

        Console.WriteLine("Building Server (Linux)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Server (Linux).");
    }


    [MenuItem("Build/Build Client (Windows Mono)")]
    public static void BuildMonoWindowsClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client/Mono/Attheria.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Windows Mono)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Windows Mono).");
    }

    [MenuItem("Build/Build Client (Linux Mono)")]
    public static void BuildMonoLinuxClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client_Linux/Mono/Attheria",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Linux Mono)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Linux Mono).");
    }

    [MenuItem("Build/Build Client (Windows Il2cpp)")]
    public static void BuildIl2cppWindowsClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client/Il2cpp/Attheria.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Windows Il2cpp)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Windows Il2cpp).");
    }

    [MenuItem("Build/Build Client (Linux Il2cpp)")]
    public static void BuildIl2cppLinuxClient()
    {
        BuildPlayerOptions buildPlayerOptions = new()
        {
            scenes = new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/Attheria.unity" },
            locationPathName = $"{Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName)}/Builds/Client_Linux/Il2cpp/Attheria",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.CompressWithLz4HC,
            subtarget = (int)StandaloneBuildSubtarget.Player
        };

        Console.WriteLine("Building Client (Linux Il2cpp)...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Console.WriteLine("Built Client (Linux Il2cpp).");
    }
#endif
}