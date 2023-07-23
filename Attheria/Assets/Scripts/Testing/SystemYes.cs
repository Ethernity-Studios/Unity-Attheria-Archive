using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SystemYes : MonoBehaviour, ISaveable
{
    [SerializeField] private int Level = 1;

    public List<int> GEJ;

    public List<TestENemi> Enemies;
    public List<TestENemi.SavableData> SavableEnemies;
    
    public object SaveData() => new SavableData()
    {
        level = Level,
        GEJ = GEJ,
        enemies = Enemies.Select(enemy => enemy.Data).ToList()
    };

    public Task LoadData(object data)
    {
        var saveData = (SavableData)data;

        Level = saveData.level;
        GEJ = saveData.GEJ;
        SavableEnemies = saveData.enemies;

        Debug.Log("Data on systemyes loaded!");

        return Task.CompletedTask;
    }
    
    [Serializable]
    private struct SavableData
    {
        public List<int> GEJ;
        public int level; 
        public List<TestENemi.SavableData> enemies;
    }


}



