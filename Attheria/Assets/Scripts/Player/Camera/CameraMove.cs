using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform HeadPosition;

    private void Update()
    {
        transform.position = HeadPosition.position;
    }
}
