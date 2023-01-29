using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = FileLocationConstants.BehaviourDecisions + "/SensorCheck")]
public class SensorCheck : Decision
{
    public override bool Decide(EnemyController controller)
    {
        return controller.m_Sensor.Target != null;
    }
}
