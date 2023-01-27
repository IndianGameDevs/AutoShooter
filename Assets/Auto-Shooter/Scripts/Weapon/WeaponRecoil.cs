using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public LookAt cameraAim;
    public float Horizontal;
    public float Vertical;

    public WeaponRecoilPattern activeRecoilPattern;

    private int index = 0;
 [SerializeField]private float recoilDuration;
    private float currentRecoilTime;
    public void ResetRecoil()
    {
        index = 0;
    }

    public void GenerateRecoil()
    {
        currentRecoilTime = recoilDuration;
        Vector2 Pos = activeRecoilPattern.EvaluateRecoil(index);
        Horizontal = (Pos.x);
        Vertical = (Pos.y);
        index++;
    }
    public void UpdateWeaponRecoil(float deltaTime)
    {
        if (currentRecoilTime > 0)
        {
            cameraAim.rollAxis -= (Horizontal / 10 * deltaTime) / recoilDuration;
            cameraAim.yawAxis -= (Vertical / 10 * deltaTime) / recoilDuration;
        }

        currentRecoilTime -= deltaTime;
    }
}
