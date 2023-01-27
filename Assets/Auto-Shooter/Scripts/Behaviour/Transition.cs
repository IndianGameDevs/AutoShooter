using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    public Decision decision;

    public State m_TrueState;

    public State m_FalseState;
}
