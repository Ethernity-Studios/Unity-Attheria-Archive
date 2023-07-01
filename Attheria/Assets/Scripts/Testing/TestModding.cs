using UnityEngine;
using V8.Net;

public class TestModding : MonoBehaviour
{
    void Start()
    {
        V8Engine e = new();
        e.Execute("function foo(s) { /* Some JavaScript Code Here */ return s; }", "My V8.NET Console");
        Handle result = e.DynamicGlobalObject.foo("Ahoj");
        Debug.Log(result.ToString());


        Debug.Log("Hello");

    }

    void Update()
    {
        
    }
}
;