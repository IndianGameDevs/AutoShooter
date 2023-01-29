using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = FileLocationConstants.BehaviourDecisions + "/IsEnemyInRange")]
public class IsEnemyInRange : Decision
{
    public override bool Decide(EnemyController controller)
    {
        return CheckEnemyInRange(controller);
    }

    private bool CheckEnemyInRange(EnemyController controller)
    {
        if (controller.m_Sensor.Target == null) return false;
        float distance = Vector3.Distance(controller.m_Sensor.Target.position, controller.transform.position);
        return distance <= controller.enemyAgent.stoppingDistance +.5f;
    }
}