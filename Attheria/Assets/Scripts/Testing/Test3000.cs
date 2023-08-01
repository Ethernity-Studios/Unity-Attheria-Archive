using System;
using System.Threading.Tasks;
using Managers;

public class Test3000 : Manager
{
    private void Awake()
    {
        UnityEditor.EditorGUIUtility.PingObject(null);
    }

    public string Sliggy;
    public override object SaveData() => new SavableData()
    {
        Sliggy = Sliggy
    };

    public override Task LoadData(object data)
    {
        var saveData = (SavableData)data;

        Sliggy = saveData.Sliggy;

        return Task.CompletedTask;
    }
    
    [Serializable]
    private struct SavableData
    {
        public string Sliggy;
    }
}
