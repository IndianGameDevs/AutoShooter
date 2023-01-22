using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Movement
{
    [Range(0.1f,25.0f)]
    public float MovementSpeed;

    [Range(0.1f, 50.0f)]
    public float RunSpeed;

    [Range(0.1f, 30.0f)]
    public float TurnSpeed;

    [Range(0.1f, 15.0f)]
    public float JumpSpeed;

    [Range(0.1f, 30.0f)]
    public float AirControl;

    [Range(0.1f, 20.0f)]
    public float Gravity;

    [Range(0.0f, 1.0f)]
    public float stepDown;

}
