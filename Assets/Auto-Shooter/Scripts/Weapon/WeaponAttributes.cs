using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponAttributes
{
    public ParticleSystem m_Muzzle;
    public TrailRenderer m_BulletEffect;
    public int MagazineSize;
    public int MaxBulletBounces;
    public float BulletSpeed;
    public float BulletDrop;
    public float BulletLifeTime;
    public float WeaponFireRate;
}
