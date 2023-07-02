using System.Collections.Generic;
using Microsoft.ClearScript.V8;
using UnityEngine;
using Object = System.Object;

public class EngineManager: MonoBehaviour {
    private Dictionary<string, V8ScriptEngine> engines = new();

    public void ExecuteIsolated(string code, string tag) {
        var engine = BootstrapEngine();
        
        engines.Add(tag, engine);
        
        engine.Execute(code);
    }

    public string SpawnInstance() {
        // TODO

        return null;
    }

    public Object EvaluateIsolated(string code) {
        var engine = BootstrapEngine();

        return engine.Evaluate(code);
    }


    public void ExecuteInSpecific(string code, string tag) => engines[tag]?.Execute(code);

    public Object EvaluateSpecific(string code, string tag) => engines[tag]?.Evaluate(code);


    public void Dispose(string tag) {
        var engine = engines[tag];
        engine?.Interrupt();
        engine?.Dispose();
        engines.Remove(tag);
    }

    V8ScriptEngine BootstrapEngine() => new V8ScriptEngine();


    private void Start() {
        ExecuteIsolated("let x = 1", "a");
        var res = EvaluateIsolated("x");
        Debug.Log(res.ToString());
    }
}