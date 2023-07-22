using System;
using UnityEngine;

public class TestENemi : MonoBehaviour
{
    public string Name;

    public int Hp;

    public SavableData Data;

    private void Start()
    {
        Data = new()
        {
            name = Name,
            hp = Hp,
            position = transform.position
        };
    }

    [Serializable]
    public struct SavableData
    {
        public string name;
        public int hp;
        public Vector3 position;
    }
}



