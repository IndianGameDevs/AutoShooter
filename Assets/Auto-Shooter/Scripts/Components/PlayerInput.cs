using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerInput
{
    public float Horizontal;
    public float Forward;

    public bool JumpPressed;
    public bool RunPressed;

    public bool ShootPressed;
}
