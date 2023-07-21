using System;
using System.Reflection;
using Mirror;
using SaveSystem.Migrations;
using UnityEngine;

public class SaveLoadManager : NetworkBehaviour
{
    public static SaveLoadManager Instance;
    
    public event DataLoadedDelegate DataLoaded;
    public delegate void DataLoadedDelegate();
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        
        Invoke(nameof(onDataLoaded),1f);
    }

    /// <summary>
    /// Invoke event when all data is loaded
    /// </summary>
    private void onDataLoaded()
    {
        DataLoaded?.Invoke();
        Debug.Log("Data loaded!");

    }

   // [Migration(test = "xDD", testFloat = 3)]
    //public string ta;

    private void Start()
    {
        GetAttribute();
    }

    public void GetAttribute()
    {
        //FieldInfo fieldInfo = typeof(SavableData).GetProperty("");
        //var propertyInfo = typeof(SavableData).GetProperty("level");
        //var test = (Migration)propertyInfo.GetCustomAttribute(typeof(Migration));
           // Debug.Log("Mam to " + test.test + " " + test.testFloat);

           Type type = typeof(SaveLoadManager);
           FieldInfo[] fields = type.GetFields();

           foreach (var field in fields)
           {

               object[] attributes = field.GetCustomAttributes(typeof(MigrationAttribute), false);
               foreach (var a in attributes)
               {
                   if (a is not MigrationAttribute migrationAttribute) continue;
                   Debug.Log("Field name: " + field.Name);

                   Debug.Log("TEST " + migrationAttribute.test + migrationAttribute.testFloat);
               }
           }
    }
    
    [Migration(test = "TESRRTAAD", testFloat = 69)]
    public int level;
    [Migration(test = "JMENO BEST", testFloat = 69000)]
    public int name;
    
    [Serializable]
    public struct SavableData
    {
        [Migration(test = "TESRRTAAD", testFloat = 69)]
        public int level;
        [Migration(test = "JMENO BEST", testFloat = 69000)]
        public int name;
    }
}
