using System.IO;
using UnityEngine;

public class EEEE : MonoBehaviour
{
    void Start()
    {
        //Debug.Log("Menu start");
        //Debug.Log(Application.dataPath[..Application.dataPath.LastIndexOf('/')]);
        Debug.Log(Directory.GetParent(Directory.GetParent(Application.dataPath)!.FullName));

    }
}
