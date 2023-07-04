using System;
using UnityEngine;

public class SystemYes : MonoBehaviour, ISavable
{
    [SerializeField] private int Level = 1;
    
    
    public object CaptureState()
    {
        return new SavableData()
        {
            level = Level
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SavableData)state;

        Level = saveData.level;
    }
    
    [Serializable]
    private struct SavableData
    {
        public int level;
    }
}
