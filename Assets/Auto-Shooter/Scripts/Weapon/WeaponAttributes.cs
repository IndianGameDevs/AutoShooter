using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponAttributes
{
    public ParticleSystem m_Muzzle;
    public ParticleSystem m_HitEffect;
    public TrailRenderer m_BulletEffect;

    [Space(20)]
    [Header("ScriptableObject")]
    [Tooltip("Weapon Recoil System (Create a Scriptable Object and Add it)")]
    public WeaponRecoilPattern weaponRecoil;
    [Space(20)]
    public int MagazineSize;
    public int MaxBulletBounces;
    public float BulletSpeed;
    public float BulletDrop;
    public float BulletLifeTime;
    public float WeaponFireRate;

    public int bulletDamage;
}
