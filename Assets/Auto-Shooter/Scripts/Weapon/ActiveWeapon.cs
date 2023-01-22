using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public WeaponBase currentActiveWeapon;
    public WeaponBase[] PrimaryWeapons = new WeaponBase[2];
    public WeaponBase SecondaryWeapon;
    public WeaponBase MeleeWeapon;

    private bool IsTapped;
    private void Update()
    {
        if (currentActiveWeapon == null) return;
        switch (currentActiveWeapon.weaponInput)
        {
            case WeaponInput.Hold: CheckForHoldInput(); break;
            case WeaponInput.Tap: CheckForTapInput(); break;
        }
        currentActiveWeapon.UpdateWeapon(Time.deltaTime);
    }

    private void CheckForTapInput()
    {
        if (PlayerInputHandler.Instance.IsShootPressed && !IsTapped)
        {
            currentActiveWeapon.StartAttacking();
            IsTapped = true;
        }
        else
        {
            currentActiveWeapon.StopAttacking();
            IsTapped = false;
        }
    }

    private void CheckForHoldInput()
    {
        if (PlayerInputHandler.Instance.IsShootPressed)
        {
            currentActiveWeapon.StartAttacking();
        }
        else
        {
            currentActiveWeapon.StopAttacking();
        }
    }
}
