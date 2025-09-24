using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelState : State
{
    private float m_aggroRange;

    private float m_channelTime;
    public override void OnStart()
    {
        m_aggroRange = UnitInfo.UnitData.Range;
        m_channelTime = UnitInfo.UnitData.AttackSpeed;

        if (Agent)
            Agent.isStopped = true;
    }

    public override void OnUpdate()
    {
        if (UnitStateMachine.Target == null)
        {
            //UnitStateMachine.FindTarget();
            UnitStateMachine.ChangeState(UnitStates.Idle);
            return;
        }

        if (Vector2.Distance(transform.position, UnitStateMachine.Target.transform.position) >= m_aggroRange)
        {
            UnitStateMachine.ChangeState(UnitStates.Moving);
        }

        if (m_channelTime > 0.0f)
        {
            m_channelTime -= Time.deltaTime;
        }
        else
        {
            m_channelTime = 0.0f;
            UnitStateMachine.ChangeState(UnitStates.Attacking);
        }
    }

    public override void OnExit()
    {
        
    }
}
