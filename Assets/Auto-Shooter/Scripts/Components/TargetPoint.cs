using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public Transform[] HitPoints;

    [SerializeField] private bool ShowGizmo;

    private void OnDrawGizmos()
    {
        if (!ShowGizmo) return;

        foreach(Transform hit in HitPoints)
        {
            if(hit)
            {
                Gizmos.DrawWireSphere(hit.position, 0.5f);
            }
        }
    }
}
