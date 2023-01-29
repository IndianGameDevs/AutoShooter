using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = FileLocationConstants.BehaviourExitActions + "/StopShooting")]
public class StopEnemyShooting : Action
{
    public override void Act(EnemyController controller)
    {
        if(controller.currentWeapon!= null)
        {
            controller.currentWeapon.StopAttacking();
        }
    }
}
