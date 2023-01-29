using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot : EnemyController
{
    [SerializeField] private bool ShowGizmos;
    public override void ControllerUpdate()
    {
        base.ControllerUpdate();
        m_Animator.SetBool("Walk", !enemyAgent.isStopped);
    }

    public override void UpdateCurrentHealth(int prevHealth, int curHealth, int maxHealth)
    {
        base.UpdateCurrentHealth(prevHealth, curHealth, maxHealth);
        m_Animator.ResetTrigger("Hit");
        m_Animator.SetTrigger("Hit");
    }
    private void OnDrawGizmos()
    {
        if (ShowGizmos)
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < patrolPoints.Length - 1; i++)
            {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
            }
        }
    }
}
