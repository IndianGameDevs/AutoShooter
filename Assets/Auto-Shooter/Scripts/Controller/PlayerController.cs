using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Collider characterCollider;
    public Transform cameraAimLookAt;
    [Header("Scene-Objects")]
    [SerializeField] private CameraProjection playerCam;
    [Header("Attributes")]
    [SerializeField] private CameraAim m_CameraAimOffset;
    [Range(1f,100.0f)]
    public float m_CameraSensitivity;
    [SerializeField] private Movement m_Movement;
    public PlayerAnimator m_PlayerAnimator;
    [SerializeField] private PlayerInput m_Input;
    [Space(20)]
    private PlayerInputHandler inputHandler;

    public bool IsJumping;
    public bool IsRunning;

    private Vector3 move;
    public LookAt aimLookAt;
    public Health playerHealth;

    private float m_CharacterSpeed;
    [SerializeField] private MovementMode m_MovementMode;

    private void Awake()
    {
        playerCam = FindObjectOfType<CameraProjection>();
        aimLookAt = FindObjectOfType<LookAt>();

        inputHandler = PlayerInputHandler.Instance;
        m_PlayerAnimator.GenerateHash();
    }

    private void Start()
    {
        playerHealth.maxHealth = 100;
        playerHealth.UpdateCurrentHealth(100);
    }
    private void Update()
    {
        CollectInput();

        if (m_Input.JumpPressed)
        {
            TryJump();
        }

        if (m_Input.RunPressed)
        {
            if (CanRun())
            {
                IsRunning = true;
            }
            else
            {
                IsRunning = false;
            }
        }
        else
        {
            IsRunning = false;
        }

        m_MovementMode = IsRunning ? MovementMode.Run : MovementMode.Walk;
        m_CharacterSpeed = m_MovementMode == MovementMode.Walk ? m_Movement.MovementSpeed : m_Movement.RunSpeed;
        RotatePlayer();

        CheckAnimations();
    }

    public void UpdatedHealth(int prev, int current, int maxHealth)
    {
        if (prev < current)
        {
            // Healed
        }
        else
        {

        }
    }

    private void RotatePlayer()
    {
        cameraAimLookAt.transform.position = playerCam.cameraAim.transform.position + m_CameraAimOffset.cameraAimOffset;

        float yawCamera = playerCam.transform.eulerAngles.y;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), m_Movement.TurnSpeed * Time.deltaTime);
    }

    private void CheckAnimations()
    {
        m_PlayerAnimator.SetGrounded(controller.isGrounded);

        m_PlayerAnimator.SetJumping(IsJumping);

        m_PlayerAnimator.SetRunning(IsRunning);

        m_PlayerAnimator.SetFall(controller.velocity.y < -.2f);

        if (controller.isGrounded)
        {
            m_PlayerAnimator.SetMovement(new Vector2(m_Input.Horizontal, m_Input.Forward));
        }
    }

    private bool CanRun()
    {
        return !IsJumping && controller.isGrounded;
    }

    private void FixedUpdate()
    {
        if (!IsJumping)
        {
            UpdateOnGround();
        }
        else
        {
            UpdateInAir();
        }
    }

    private void TryJump()
    {
        if (IsJumping || !controller.isGrounded) return;

        float jumpVel = Mathf.Sqrt(2 * m_Movement.Gravity * m_Movement.JumpSpeed);
        JumpInAir(jumpVel);

    }

    private void UpdateInAir()
    {
        move.y -= m_Movement.Gravity * Time.fixedDeltaTime;
        Vector3 displacement = move;
        displacement += (transform.forward * m_Input.Forward + transform.right * m_Input.Horizontal) * m_Movement.AirControl;
        controller.Move(displacement * Time.fixedDeltaTime);
        IsJumping = !controller.isGrounded;
    }

    private void UpdateOnGround()
    {
        Vector3 stepDownMovement = Vector3.down * m_Movement.stepDown;
        Vector3 stepForwardMovement = (transform.forward * m_Input.Forward + transform.right * m_Input.Horizontal) * m_CharacterSpeed;
        controller.Move(Time.fixedDeltaTime * (stepDownMovement + stepForwardMovement));
        if (!controller.isGrounded)
        {
            JumpInAir(0.0f);
        }
    }

    private void JumpInAir(float jumpVelocity)
    {
        move = (transform.forward * m_Input.Forward + transform.right * m_Input.Horizontal) * m_CharacterSpeed;
        move.y = jumpVelocity;
        IsJumping = true;
    }

    private void CollectInput()
    {
        m_Input.Horizontal = inputHandler.Horizontal;
        m_Input.Forward = inputHandler.Forward;
        m_Input.JumpPressed = inputHandler.IsJumpPressed;
        m_Input.RunPressed = inputHandler.IsRunPressed;
        m_Input.ShootPressed = inputHandler.IsShootPressed;
    }
}

public enum MovementMode
{
    Walk,
    Run
}
