using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private bool m_buildingBehavior;

    private float m_idleTime;
    private float m_idleCounter;
    public override void OnIntialization()
    {
        base.OnIntialization();
    }

    public override void OnStart()
    {
        m_idleTime = UnitInfo.UnitData.IdleTime;
    }

    public override void OnUpdate()
    {
        if (m_idleCounter <= m_idleTime)
        {
            m_idleCounter += Time.deltaTime;
        }
        else
        {
            m_idleCounter = 0;
            DecideAction();
        }
    }

    private void DecideAction()
    {
        if (UnitStateMachine.TargetInfo.HasInfo == false)
        {
            UnitStateMachine.FindTarget();
            return;
        }

        UnitStateMachine.ChangeState(UnitStates.Channeling);
    }
}
