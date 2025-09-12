using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementState : State
{
    //private NavMeshAgent m_agent;

    private float m_range;

    public override void OnIntialization()
    {
        base.OnIntialization();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }
    public override void OnStart()
    {
        m_range = UnitInfo.UnitData.Range;
        Agent.speed = UnitInfo.UnitData.Speed;

        Agent.isStopped = false;
    }

    public override void OnUpdate()
    {
        Agent.SetDestination(UnitStateMachine.Target.transform.position);

        if (Vector2.Distance(transform.position, UnitStateMachine.Target.transform.position) <= m_range)
        {
            UnitStateMachine.ChangeState(UnitStates.Channeling);
        }
    }

    public override void OnExit()
    {
        Agent.isStopped = true;
    }
}
