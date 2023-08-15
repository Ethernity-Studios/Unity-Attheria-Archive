using System;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public ZoneData ZoneData;
}
[Serializable]
public class ZoneData
{
    public int Id;
    public string Name;
    public List<Transform> SpawnPoints;
}