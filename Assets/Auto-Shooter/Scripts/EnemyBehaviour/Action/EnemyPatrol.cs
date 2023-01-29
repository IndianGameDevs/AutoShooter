using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = FileLocationConstants.BehaviourActions + "/Patrol")]
public class EnemyPatrol : Action
{
    public override void Act(EnemyController controller)
    {
        PatrolEnemy(controller);
    }

    private static void PatrolEnemy(EnemyController controller)
    {
        NavMeshAgent agent = controller.enemyAgent;
        agent.stoppingDistance = 0.1f;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            controller.patrolIndex++;
        }

        MoveEnemy(controller);
    }

    private static void MoveEnemy(EnemyController controller)
    {
        NavMeshAgent agent = controller.enemyAgent;
        int p = controller.patrolIndex % controller.patrolPoints.Length;
        agent.destination = controller.patrolPoints[p];
        agent.isStopped = false;
        Vector3 direction = agent.destination - controller.transform.position;
        float turnAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float turnVelocity = 0;
        float angle = Mathf.SmoothDampAngle(controller.transform.eulerAngles.y, turnAngle, ref turnVelocity, 0.05f);
        controller.transform.eulerAngles = new Vector3(0, angle, 0);
    }
}