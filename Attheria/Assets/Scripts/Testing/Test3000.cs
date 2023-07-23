using System;
using System.Threading.Tasks;
using UnityEngine;

public class Test3000 : MonoBehaviour, ISaveable
{
    public string Sliggy;
    public object SaveData() => new SavableData()
    {
        Sliggy = Sliggy
    };

    public Task LoadData(object data)
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
