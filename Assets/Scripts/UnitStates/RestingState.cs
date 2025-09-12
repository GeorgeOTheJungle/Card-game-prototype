using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestingState : State
{
    private float m_restTime;

    public override void OnStart()
    {
        m_restTime = UnitInfo.UnitData.RestTime;
    }

    public override void OnUpdate()
    {
        if (m_restTime > 0.0f)
        {
            m_restTime -= Time.deltaTime;
        }
        else
        {
            m_restTime = 0.0f;
            UnitStateMachine.ChangeState(UnitStates.Channeling);
        }
    }
}
