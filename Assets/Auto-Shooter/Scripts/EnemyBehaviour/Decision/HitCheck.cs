using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = FileLocationConstants.BehaviourDecisions + "/HitCheck")]
public class HitCheck : Decision
{
    public override bool Decide(EnemyController controller)
    {
        return controller.IsShot;
    }
}
