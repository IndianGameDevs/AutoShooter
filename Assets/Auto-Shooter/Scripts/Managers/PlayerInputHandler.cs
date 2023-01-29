using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputHandler : MonoBehaviour
{
    private static PlayerInputHandler instance;

    public static PlayerInputHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerInputHandler>();
            }
            return instance;
        }
    }

    [SerializeField] private FloatingJoystick floatingJoystick;

    public float Horizontal;
    public float Forward;

    public bool IsShootPressed;
    public bool IsRunPressed;
    public bool IsJumpPressed;

    #region Touch Input for Camera Rotation
    public float InputX, InputY;
    private bool rightFingerPressed = false;
    private Vector2 startPosition;
    #endregion

    [Range(0.1f, 30.0f)]
    public float m_CameraSensitivity;

    private void Awake()
    {
        if (floatingJoystick == null)
        {
            floatingJoystick = FindObjectOfType<FloatingJoystick>();
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        Horizontal = Input.GetAxis("Horizontal");
        Forward = Input.GetAxis("Vertical");
#else
        SetHorizontal();

        SetForward();
#endif
        CalculateTouchInput();
    }

    private void CalculateTouchInput()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                if (!rightFingerPressed)
                {
                    rightFingerPressed = true;
                    startPosition = Input.mousePosition;
                }
            }
        }

        if (rightFingerPressed)
        {
            InputX = Input.mousePosition.x - startPosition.x;
            InputY = Input.mousePosition.y - startPosition.y;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rightFingerPressed = false;
            InputX = InputY = 0;
        }
#else
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            if (t.phase.Equals(TouchPhase.Began))
            {
                if (t.position.x < Screen.width / 2) continue;
                if (!rightFingerPressed)
                {
                    rightFingerPressed = true;
                    startPosition = t.position;
                    break;
                }
            }
            else if (t.phase.Equals(TouchPhase.Moved))
            {
                if (rightFingerPressed && (t.position.x > Screen.width / 2))
                {
                    InputX = t.position.x - startPosition.x;
                    InputY = t.position.y - startPosition.y;
                }
            }
            else if (t.phase.Equals(TouchPhase.Ended) || t.phase.Equals(TouchPhase.Canceled))
            {
                rightFingerPressed = false;
                startPosition = Vector2.zero;
                InputX = 0;
                InputY = 0;
            }
        }
#endif
    }


    private void SetHorizontal()
    {
        Horizontal = floatingJoystick.Horizontal;
    }

    private void SetForward()
    {
        Forward = floatingJoystick.Vertical;
    }
}