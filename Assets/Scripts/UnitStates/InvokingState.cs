using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokingState : State
{
    [SerializeField] private float m_invokeCounter;

    public override void OnStart()
    {
        m_invokeCounter = UnitInfo.UnitData.InvokeTime;
    }

    public override void OnUpdate()
    {
        if (m_invokeCounter > 0.0f)
        {
            m_invokeCounter -= Time.deltaTime;
        }
        else
        {
            UnitStateMachine.FindTarget();
            UnitStateMachine.ChangeState(UnitStates.Moving);
        }
    }
}
