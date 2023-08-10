using System;
using UnityEditor;
using UnityEngine;


public class Pinger : MonoBehaviour
{
#if UNITY_EDITOR
    private void Awake()
    {
        EditorGUIUtility.PingObject (gameObject); // JUST TO UNCOLLAPSE HIEARCHY CAUSE UNITI STUPIDO
    }
#endif
}
