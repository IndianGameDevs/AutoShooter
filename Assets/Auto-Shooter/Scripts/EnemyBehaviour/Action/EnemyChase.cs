using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = FileLocationConstants.BehaviourActions + "/EnemyChase")]
public class EnemyChase : Action
{
    public override void Act(EnemyController controller)
    {
        ChaseEnemy(controller);
    }

    private void ChaseEnemy(EnemyController controller)
    {
        Transform target = controller.m_Sensor.Target;
        if (!target) return;
        #region Rotation
        Vector3 direction = (target.position - controller.transform.position).normalized;
        float turnAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        controller.transform.rotation = Quaternion.Euler(0, turnAngle, 0);
        #endregion

        #region Agent Movement
        NavMeshAgent agent = controller.enemyAgent;
        agent.stoppingDistance = 4.0f;
        agent.destination = target.position;

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        #endregion
    }
}
