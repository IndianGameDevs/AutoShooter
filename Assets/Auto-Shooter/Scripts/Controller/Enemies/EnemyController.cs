using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    public Health enemyHealth;
    public NavMeshAgent enemyAgent;
    public Sensor enemySensor;
    public Collider enemyCollider;

    public Vector3[] patrolPoints;

    public State emptyState;
    public State currentState;

    public void SwitchState(State state)
    {
        if (!state.Equals(emptyState))
        {
            currentState = state;
        }
    }
}
