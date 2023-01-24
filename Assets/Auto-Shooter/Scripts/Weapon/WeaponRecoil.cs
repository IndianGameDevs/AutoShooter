using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public CameraAim cameraAim;
    public float Horizontal;
    public float Vertical;

    public WeaponRecoilPattern activeRecoilPattern;

    private int index = 0;
 [SerializeField]private float recoilDuration;
    private float currentRecoilTime;
    public void ResetRecoil()
    {
        index = 0;
        cameraAim.cameraAimOffset = Vector3.zero;
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
            cameraAim.cameraAimOffset.x -= (Horizontal / 10 * deltaTime) / recoilDuration;
            cameraAim.cameraAimOffset.y -= (Vertical / 10 * deltaTime) / recoilDuration;
        }

        currentRecoilTime -= deltaTime;
    }
}
