using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : WeaponBase
{
    public WeaponAttributes m_WeaponAttributes;
    public List<RaycastBullet> bullets = new List<RaycastBullet>();

    [SerializeField] private Transform m_RaycastOrigin;
    [SerializeField] private LayerMask m_LayerMask;

    private Ray ray;
    private RaycastHit hit;

    public int currentAmmo;

    private float m_LastShot = 0.0f;

    private float FireInterval
    {
        get
        {
            if (m_WeaponAttributes.WeaponFireRate == 0) return 1.0f;
            return 1.0f / m_WeaponAttributes.WeaponFireRate;
        }
    }

    public Transform cameraTarget;
    public bool IsReloading;

    private int bulletShotIndex = 0;
    private WeaponRecoil weaponRecoil;

    private void Awake()
    {
        weaponRecoil = GetComponent<WeaponRecoil>();
        weaponRecoil.activeRecoilPattern = m_WeaponAttributes.weaponRecoil;
    }

    public override void Holster()
    {
        m_IsHolstered = true;
    }

    public override void StartAttacking(Transform target)
    {
        cameraTarget = target;
        m_IsAttacking = true;
    }

    public override void StopAttacking()
    {
        cameraTarget = null;
        m_IsAttacking = false;
        bulletShotIndex = 0;
        weaponRecoil.ResetRecoil();
    }

    public override void UnHolster()
    {
        m_IsHolstered = false;
    }

    public override void UpdateWeapon(float deltaTime)
    {
        DestroyBullet();
        UpdateWeaponFiring(deltaTime);
        weaponRecoil.UpdateWeaponRecoil(deltaTime);
        SimulateBullets(deltaTime);
    }

    private void UpdateWeaponFiring(float deltaTime)
    {
        if ((Time.time > m_LastShot + FireInterval) && m_IsAttacking)
        {
            FireBullet();
            m_LastShot = Time.time;
        }
    }

    private void FireBullet()
    {
        if (currentAmmo <= 0)
        {
            return;
        }
        weaponRecoil.GenerateRecoil();
        currentAmmo--;
        if (m_WeaponAttributes.m_Muzzle != null)
        {
            m_WeaponAttributes.m_Muzzle.Emit(1);
        }
        Vector3 u = (cameraTarget.position - m_RaycastOrigin.position) * m_WeaponAttributes.BulletSpeed;
        RaycastBullet bullet = CreateBullet(m_RaycastOrigin.position, u);
        bullets.Add(bullet);
    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPositionOfBullet(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPositionOfBullet(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    private void RaycastSegment(Vector3 p0, Vector3 p1, RaycastBullet bullet)
    {
        Vector3 direction = p1 - p0;
        ray.origin = p0;
        ray.direction = direction.normalized;

        float distance = direction.magnitude;
        if (Physics.Raycast(ray, out hit, distance, ~m_LayerMask))
        {
            bullet.bulletProjectile.transform.position = hit.point;
            bullet.time = m_WeaponAttributes.BulletLifeTime;
            if (bullet.bounces > 0)
            {
                bullet.time = 0;
                bullet.initialPosition = hit.point;
                bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, hit.normal);
                bullet.bounces--;
            }
            if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddForceAtPosition(ray.direction * 5, hit.point, ForceMode.Impulse);
            }

            m_WeaponAttributes.m_HitEffect.transform.position = hit.point;
            m_WeaponAttributes.m_HitEffect.transform.forward = hit.normal;
            m_WeaponAttributes.m_HitEffect.Emit(1);
        }
        else
        {
            bullet.bulletProjectile.transform.position = p1;
        }
    }

    private void DestroyBullet()
    {
        bullets.RemoveAll(bullet => bullet.time >= m_WeaponAttributes.BulletLifeTime);
    }

    #region Bullet Calculations
    private Vector3 GetPositionOfBullet(RaycastBullet bullet)
    {
        Vector3 gravity = Vector3.down * m_WeaponAttributes.BulletDrop;
        return bullet.initialPosition + bullet.initialVelocity * bullet.time +(0.5f * bullet.time * bullet.time * gravity);
    }

    private RaycastBullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        RaycastBullet bullet = new RaycastBullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.bounces = m_WeaponAttributes.MaxBulletBounces;
        bullet.bulletProjectile = Instantiate(m_WeaponAttributes.m_BulletEffect, position, Quaternion.identity);
        bullet.bulletProjectile.AddPosition(position);
        return bullet;
    }

    public void ReloadRaycastWeapon()
    {

    }

    public override bool CanAttack()
    {
        return !IsReloading;
    }

    public override void InitializePlayerWeapon(PlayerController playerController)
    {
        weaponRecoil.cameraAim = playerController.GetComponent<CameraAim>();
    }
    #endregion
}