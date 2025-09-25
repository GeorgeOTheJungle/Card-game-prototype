using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementState : State
{
    private float m_range;

    public override void OnStart()
    {
        m_range = UnitInfo.UnitData.Range;
        Agent.speed = UnitInfo.UnitData.Speed;

        Agent.isStopped = false;
    }

    public override void OnUpdate()
    {
        if (UnitStateMachine.TargetInfo.HasInfo)
        {
            Agent.isStopped = false;
            Agent.SetDestination(UnitStateMachine.TargetInfo.UnitTransform.position);
        }
        else
        {
            Agent.isStopped = true;
            UnitStateMachine.ChangeState(UnitStates.Idle);
            return;
        }

        if (Vector2.Distance(transform.position, UnitStateMachine.TargetInfo.UnitTransform.position) <= m_range)
        {
            UnitStateMachine.ChangeState(UnitStates.Channeling);
        }
    }

    public override void OnExit()
    {
        Agent.isStopped = true;
    }
}
