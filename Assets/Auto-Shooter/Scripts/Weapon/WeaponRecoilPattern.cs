using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Weapons/Recoil")]
public class WeaponRecoilPattern : ScriptableObject
{
    public string weaponName;
    public Vector2[] recoilPattern;

    public Vector2 EvaluateRecoil(int index)
    {
        return recoilPattern[index % recoilPattern.Length];
    }
}
