using System;
using Mirror;
using UnityEngine;

/// <summary>
/// Result for confirmation screen
/// </summary>
public enum Result
{
    None = 0, Confirm = 1, Cancel = 2
}

public enum Gender
{
    None = 0, Male = 1, Female = 2
}

public enum CameraType
{
    FirstPerson, ThirdPersonLocked, ThirdPersonFree
}

public enum MovementState
{
   idle, walking, sprinting, crouching, crawling, swimming, air
}