using UnityEngine;
using System;
//using V8.Net;
using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;

public class TestModding : MonoBehaviour
{
    void Start()
    {
        //V8Engine e = new();
        //e.Execute("function foo(s) { /* Some JavaScript Code Here */ return s; }", "My V8.NET Console");
        //Handle result = e.DynamicGlobalObject.foo("Ahoj");
        //Debug.Log(result.ToString());
        
        Debug.Log("Hello");


        using V8ScriptEngine engine = new V8ScriptEngine();
        // expose a host type
        engine.AddHostType("Debug", typeof(Debug));
        engine.Execute("Debug.Log('TESTTTTTTT')");
    }

    public int GetPlayerHealth()
    {
        return 10;
    }

    void Update()
    {
        
    }
}
