using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BugReporting : MonoBehaviour
{
    [SerializeField] private TMP_InputField MessageInput;
    [SerializeField] private Toggle ScreenshotToggle;

    public string ScreenshotPath = @"";
    
    GlobalInput input;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    void toggle()
    {
        
    }

    /// <summary>
    /// TODO 
    /// </summary>
    public void Send()
    {
        
    }
}
