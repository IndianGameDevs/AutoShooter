using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    public Health enemyHealth;
    public NavMeshAgent enemyAgent;
    public Collider enemyCollider;
    public Animator m_Animator;
    public Vector3[] patrolPoints;
    public Transform[] patrolSpawns;

    public WeaponBase currentWeapon;

    public int patrolIndex;

    public State emptyState;
    public State currentState;

    public Transform m_TargetPoint;
    public Sensor m_Sensor;

    [SerializeField] private Vector3 targetOffset;

    #region setters-getters
    public bool IsDead
    {
        get
        {
            return currentHealth > 0;
        }
    }
    public float currentHealth;
    public bool IsShot;
    public float recoveryTime = 1.0f;
    public float recoveryCountDown;
    #endregion

    private void Awake()
    {
        int length = patrolSpawns.Length;
        patrolPoints = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            patrolPoints[i] = patrolSpawns[i].position;
        }
    }

    public virtual void UpdateCurrentHealth(int prevHealth, int curHealth, int maxHealth)
    {
        currentHealth = curHealth;
        recoveryCountDown = recoveryTime;
        IsShot = true;
    }

    public void SwitchState(State state)
    {
        if (!state.Equals(emptyState))
        {
            currentState.Exit(this);
            currentState = state;
        }
    }

    private void Update()
    {
        ControllerUpdate();
    }

    public virtual void ControllerUpdate()
    {
        if (currentState != null)
            currentState.UpdateState(this);

        if (currentWeapon != null)
            currentWeapon.UpdateWeapon(Time.deltaTime);

        if (m_Sensor.Target == null)
        {
            m_TargetPoint.position = transform.position + (transform.forward + Vector3.up * 1.4f);
        }
        else
        {
            m_TargetPoint.position = m_Sensor.Target.gameObject.transform.TransformPoint(targetOffset);
        }
    }
}
