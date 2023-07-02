using UnityEngine;
using System;
using System.Collections.Generic;

using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;

public class TestModding : MonoBehaviour
{
     static List<IJavaScriptObject> fns = new();
     V8ScriptEngine engine = new();

    void Start() {

        onJumpEvent += TestMethod;

        //V8Engine e = new();
        //e.Execute("function foo(s) { /* Some JavaScript Code Here */ return s; }", "My V8.NET Console");
        //Handle result = e.DynamicGlobalObject.foo("Ahoj");
        //Debug.Log(result.ToString());

        Debug.Log("Hello");

        // expose a host type
        engine.AddHostType("Debug", typeof(Debug));
        engine.AddHostType("testModding", typeof(TestModding));
        engine.AddHostType("UwU", typeof(Physics));
        engine.Execute("Debug.Log(testModding.getPos()); " +
                       "UwU.gravity = testModding.getPos();" +
                       "Debug.Log(testModding.gethp());");
        engine.Execute("XD", false, "idk = (e)=>{Debug.Log(e)};" +
                                    "testModding.onJump(idk);");
        engine.AddHostObject("OwO", this);
        engine.Execute("Debug.Log(OwO.playerHealth)");
    }

    public int playerHealth = 69;

    public static int gethp()
    {
        return 5;
    }

    public static Vector3 getPos()
    {
        return new Vector3(0, 10, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            onJumpEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public static void onJump(IJavaScriptObject o)
    {
        fns.Add(o);
    }

    public event EventHandler onJumpEvent;


    void TestMethod(object sender, EventArgs e)
    {
        foreach (var VARIABLE in fns)
        {
            VARIABLE.InvokeAsFunction("Player jumped");
        }

        //Debug.Log("Player jumped");
    }
}