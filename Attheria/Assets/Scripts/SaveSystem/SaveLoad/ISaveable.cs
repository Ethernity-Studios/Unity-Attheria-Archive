using System.Threading.Tasks;
using UnityEngine;

public interface ISaveable
{
    object SaveData();
    Task LoadData(object data);
}
