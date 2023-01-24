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
    public Transform aimTarget;

    private void Start()
    {
        aimTarget = this.GetComponent<PlayerController>().cameraAimLookAt;
       if(currentActiveWeapon!= null)
        {
            currentActiveWeapon.InitializePlayerWeapon(this.GetComponent<PlayerController>());
        }
    }
    private void Update()
    {
        if (currentActiveWeapon == null) return;
        switch (currentActiveWeapon.weaponInput)
        {
            case WeaponInput.Hold: CheckForHoldInput(); break;
            case WeaponInput.Tap: CheckForTapInput(); break;
        }
        currentActiveWeapon.UpdateWeapon(Time.deltaTime);
        if (currentActiveWeapon.m_IsAttacking)
        {

        }
        else
        {

        }
    }


    #region Weapon Attack
    private void CheckForTapInput()
    {
        if (PlayerInputHandler.Instance.IsShootPressed && !IsTapped)
        {
            currentActiveWeapon.StartAttacking(aimTarget);
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
            currentActiveWeapon.StartAttacking(aimTarget);
        }
        else
        {
            currentActiveWeapon.StopAttacking();
        }
    }

    #endregion
}
