using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaycastBullet
{
    public float time;
    public int bounces;
    public Vector3 initialPosition;
    public Vector3 initialVelocity;
    public TrailRenderer bulletProjectile;
}
