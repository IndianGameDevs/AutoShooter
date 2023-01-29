using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGizmos : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private float CubeSize;
    [SerializeField] private bool m_Show;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!m_Show) return;
        if (point == null) return;
        Gizmos.color = color;
        Gizmos.DrawCube(point.position, Vector3.one * CubeSize);
    }
#endif
}
