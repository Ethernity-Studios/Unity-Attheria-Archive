using System;
using UnityEngine;

public class Test3000 : MonoBehaviour, ISaveable
{
    public string Sliggy;
    public object SaveData() => new SavableData()
    {
        Sliggy = Sliggy
    };

    public void LoadData(object data)
    {
        var saveData = (SavableData)data;

        Sliggy = saveData.Sliggy;
    }
    
    [Serializable]
    private struct SavableData
    {
        public string Sliggy;
    }
}
