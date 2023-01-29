using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = FileLocationConstants.BehaviourActions + "/RifleAttack")]
public class EnemyRifleAttack : Action
{
    public override void Act(EnemyController controller)
    {
        if (controller.currentWeapon == null) return;

        controller.enemyAgent.isStopped = true;

        if (controller.m_Sensor.Target == null) return;
        Vector3 direction = controller.m_Sensor.Target.position - controller.transform.position;
        float turnAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float turnVelocity = 0;
        float angle = Mathf.SmoothDampAngle(controller.transform.eulerAngles.y, turnAngle, ref turnVelocity, 0.05f);
        controller.transform.eulerAngles = new Vector3(0, angle, 0);
        controller.currentWeapon.StartAttacking(controller.m_Sensor.Target);
    }
}