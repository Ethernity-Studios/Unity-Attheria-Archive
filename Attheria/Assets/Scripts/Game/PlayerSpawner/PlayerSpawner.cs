using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    [SerializeField] private GameObject ZoneInstance;
    [SerializeField] private RectTransform ZoneHolder;

    public List<Zone> Zones;

    [SerializeField] private GameObject PlayerSpawn;

    public ZoneData SelectedZoneData = null;

    private List<GameObject> ZonePings;
    [SerializeField] private GameObject ZonePing;
    [SerializeField] private Transform ZonePingHolder;

    private void Start()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void OpenSpawner(List<int> zones)
    {
        PlayerSpawn.SetActive(true);

        LoadZones(zones);
    }

    public void Spawn()
    {
        if (SelectedZoneData.Id == -1) //TODO spawnable location e.g. - bed
        {
            
            //No spawnpoint selected
        }
        else
        {
            PlayerSpawn.SetActive(false);
            NetworkClient.AddPlayer(SelectedZoneData.Id);
        }
    }

    public void LoadZones(List<int> zones)
    {
        foreach (Transform zone in ZoneHolder)
        {
            Destroy(zone.gameObject);
        }

        bool canSpawnZone = true;
        zones ??= new() { -1 };
        foreach (var zone in Zones)
        {
            if (!zones.Contains(-1))
            {
                foreach (int zoneId in zones)
                {
                    if (zone.ZoneData.Id == zoneId)
                    {
                        canSpawnZone = true;
                        break;
                    }
                    else canSpawnZone = false;
                }
            }

            if (!canSpawnZone) continue;
            GameObject z = Instantiate(ZoneInstance, ZoneHolder);
            z.transform.localScale = Vector3.one;
            ZoneInstance instance = z.GetComponent<ZoneInstance>();
            instance.Name.text = zone.ZoneData.Name;
            instance.ZoneData = zone.ZoneData;
        }
    }

    public void OnZoneClick(ZoneData data)
    {
        SelectedZoneData = data;
        spawnZonePing(data.Id);
    }
    
    void spawnZonePing(int id)
    {
        ZonePings ??= new();
        if(ZonePings.Count != 0)
        {
            Destroy(ZonePings[0]);
            ZonePings.Remove(ZonePings[0]);
        }

        GameObject ping = Instantiate(ZonePing, ZonePingHolder);
        ping.transform.localScale = Vector3.one;

        Zone zone = Zones.FirstOrDefault(x => x.ZoneData.Id == id);
        ping.GetComponent<RectTransform>().anchoredPosition = new Vector2(zone!.transform.localPosition.x/10,zone!.transform.localPosition.z/10);
        ZonePings.Add(ping);
    }
}

public static class ZoneReaderWriter
{
    public static void WriteZone(this NetworkWriter writer, ZoneData zoneData)
    {
        writer.WriteInt(zoneData.Id);
        writer.WriteString(zoneData.Name);
        writer.WriteList<Transform>(zoneData.SpawnPoints);
    }

    public static ZoneData ReadZone(this NetworkReader reader)
    {
        return new ZoneData()
        {
            Id = reader.ReadInt(),
            Name = reader.ReadString(),
            SpawnPoints = reader.ReadList<Transform>()
        };
    }
}
