using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void OnIntialization()
    {
        base.OnIntialization();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    public override void OnUpdate()
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
}
