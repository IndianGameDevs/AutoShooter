using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProjection : MonoBehaviour
{
    public Transform cameraAim;
    public LayerMask playerMask;

    private Ray aimRay;
    private RaycastHit hit;

    private void Update()
    {
        aimRay.origin = transform.position;
        aimRay.direction = cameraAim.position - transform.position;
        if (Physics.Raycast(aimRay, out hit, 100.0f, ~playerMask))
        {
            cameraAim.position = hit.point;
        }
        else
        {
            cameraAim.position = transform.position + transform.forward * 100.0f;
        }
    }
}
