using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProjection : MonoBehaviour
{
    public Transform cameraAim;
    public LayerMask playerMask;

    private Ray aimRay;
    private RaycastHit hit;

    [Range(5.0f, 50.0f)]
    public float maxRange;

    public Transform m_LookAt;

    private void Update()
    {
        aimRay.origin = transform.position;
        aimRay.direction = cameraAim.position - transform.position;
        if (Physics.Raycast(aimRay, out hit, maxRange, ~playerMask))
        {
            cameraAim.position = hit.point;
        }
        else
        {
            cameraAim.position = transform.position + transform.forward * maxRange;
        }

        
    }
}
