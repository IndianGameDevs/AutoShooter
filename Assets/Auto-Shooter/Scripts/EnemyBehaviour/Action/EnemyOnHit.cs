using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =FileLocationConstants.BehaviourActions+"/OnHit")]
public class EnemyOnHit : Action
{
    public override void Act(EnemyController controller)
    {
        controller.recoveryCountDown -= Time.unscaledDeltaTime;
        controller.enemyAgent.isStopped = true;
        if (controller.recoveryCountDown <= 0)
        {
            controller.IsShot = false;
        }
    }
}
