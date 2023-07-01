// See https://aka.ms/new-console-template for more information

using Microsoft.ClearScript.V8;

using (var engine = new V8ScriptEngine())

{

    // expose a host type

    engine.AddHostType("Console", typeof(Console));

    engine.Execute("Console.WriteLine('{0} is an interesting number.', Math.PI)");
}