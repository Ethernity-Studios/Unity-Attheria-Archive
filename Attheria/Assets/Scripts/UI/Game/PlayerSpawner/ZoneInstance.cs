using System;
using TMPro;
using UnityEngine;

public class ZoneInstance : MonoBehaviour
{
    public TMP_Text Name;
    public ZoneData ZoneData;

    public void OnClick()
    {
        PlayerSpawner.Instance.OnZoneClick(ZoneData);
    }
}
