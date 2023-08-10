using System.Threading.Tasks;
using UnityEngine;

public interface ISaveable
{
    Task<object> SaveData();
    Task LoadData(object data);
}
