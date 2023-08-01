using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Managers;
using UnityEngine;

public class SystemYes : Manager
{
    [SerializeField] private int Level = 1;

    public List<int> GEJ;

    public List<TestENemi> Enemies;
    public List<TestENemi.SavableData> SavableEnemies;
    
    public override object SaveData() => new SavableData()
    {
        level = Level,
        GEJ = GEJ,
        enemies = Enemies.Select(enemy => enemy.Data).ToList()
    };

    public override Task LoadData(object data)
    {
        var saveData = (SavableData)data;

        Level = saveData.level;
        GEJ = saveData.GEJ;
        SavableEnemies = saveData.enemies;
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



