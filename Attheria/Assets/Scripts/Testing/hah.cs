using Mirror.FizzySteam;
using Steamworks;
using UnityEngine;

public class hah : MonoBehaviour
{
    void Start()
    {
        if (SteamManager.Initialized)
        {
            SteamUser.GetSteamID();
            SteamAPI.RestartAppIfNecessary(AppId_t.Invalid); 
        }
        

        Debug.Log(Application.persistentDataPath);

    }

    void Update()
    {
        transform.position += new Vector3(0,Random.Range(0,0.1f) * Time.deltaTime,0);
    }
}
