using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class Pinger : MonoBehaviour
{
    private void Awake()
    {
        EditorGUIUtility.PingObject (gameObject); // JUST TO UNCOLLAPSE HIEARCHY CAUSE UNITI STUPIDO
    }
}
#endif