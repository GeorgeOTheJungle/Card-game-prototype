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
        if (m_buildingBehavior)
        {
            var layerMask = LayerMask.GetMask("Unit");
            var targetCheck = Physics2D.OverlapCircleAll(transform.position, UnitInfo.UnitData.Range, layerMask);

            foreach (var target in targetCheck)
            {
                if (target.TryGetComponent(out UnitInfo unit))
                {
                    if (unit.Team == UnitInfo.Team)
                    {
                        // Ally targeting only?
                        continue;
                    }

                    UnitStateMachine.Target = unit;
                    UnitStateMachine.ChangeState(UnitStates.Channeling);
                    break;
                }
            }
        }
        else
        {
            UnitStateMachine.FindTarget();
            UnitStateMachine.ChangeState(UnitStates.Moving);
        }
    }
}
