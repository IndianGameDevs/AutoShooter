using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string m_WeaponName;
    [Space(20)]
    public bool m_IsAttacking;
    public bool m_IsHolstered;

    public WeaponInput weaponInput;

    public abstract void StartAttacking(Transform target);
    public abstract void StopAttacking();
    public abstract void Holster();
    public abstract void UnHolster();

    public abstract void UpdateWeapon(float deltaTime);

    public abstract bool CanAttack();
}
