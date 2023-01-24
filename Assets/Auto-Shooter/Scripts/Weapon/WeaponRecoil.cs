using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Weapons/Recoil")]
public class WeaponRecoil : ScriptableObject
{
    public string weaponName;
    public Vector2[] recoilPattern;
}
