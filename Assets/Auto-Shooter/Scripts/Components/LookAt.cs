using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 offset;

    public float rollAxis;
    public float yawAxis;

    private void Awake()
    {
        followTarget = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        UpdateCamera();
        transform.position = followTarget.TransformPoint(offset);
    }

    private void UpdateCamera()
    {
        rollAxis -= (PlayerInputHandler.Instance.InputY / 100) * Time.deltaTime * PlayerInputHandler.Instance.m_CameraSensitivity * 5f;
        rollAxis = Mathf.Clamp(rollAxis, -60, 60);
        yawAxis += (PlayerInputHandler.Instance.InputX / 10) * (PlayerInputHandler.Instance.m_CameraSensitivity) * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(rollAxis, yawAxis, 0);
    }
}
