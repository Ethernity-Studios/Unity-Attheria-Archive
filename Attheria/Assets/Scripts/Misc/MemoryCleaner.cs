using System;
using UnityEditor;
using UnityEngine;

public class MemoryCleaner
{
#if UNITY_EDITOR
    [MenuItem("Tools/Force Garbage Collection")]
    static void GarbageCollect()
    {
        EditorUtility.UnloadUnusedAssetsImmediate();
        GC.Collect();
    }
#endif
}