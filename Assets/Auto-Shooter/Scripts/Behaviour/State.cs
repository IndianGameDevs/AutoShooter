using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Behaviour/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;

    public void UpdateState(EnemyController controller)
    {
        PerformAction(controller);
        CheckDecision(controller);
    }

    private void PerformAction(EnemyController controller)
    {
        foreach (Action action in actions)
        {
            action.Act(controller);
        }
    }

    private void CheckDecision(EnemyController controller)
    {
        bool decision = false;

        foreach (Transition t in transitions)
        {
            decision = t.decision.Decide(controller);
            ChangeState(controller, decision ? t.m_TrueState : t.m_FalseState);
        }
    }

    private void ChangeState(EnemyController controller, State state)
    {
        controller.SwitchState(state);
    }
}
