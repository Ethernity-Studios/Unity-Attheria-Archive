using Steamworks;
using UnityEngine;

public class hah : MonoBehaviour
{
    void Start()
    {
        SteamUser.GetSteamID();
        SteamAPI.RestartAppIfNecessary(AppId_t.Invalid);
    }

    void Update()
    {
        transform.position += new Vector3(0,Random.Range(0,0.1f) * Time.deltaTime,0);
    }
}
