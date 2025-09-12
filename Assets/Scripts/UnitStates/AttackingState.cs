using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : State
{
    private float m_attackCounter;
    private float m_range;
    [SerializeField] private DamageComponent m_damageComponent;

    public override void OnStart()
    {
        m_range = UnitInfo.UnitData.Range;
        m_attackCounter = UnitInfo.UnitData.ChannelSpeed;

        float unitDamage = UnitInfo.UnitData.Damage;
        UnitInfo targetUnitInfo = UnitStateMachine.Target;
        Vector2 unitCenter = UnitInfo.UnitCenter.position;
        m_damageComponent.PerformAttack(unitDamage, targetUnitInfo, unitCenter);

        UnitStateMachine.ChangeState(UnitStates.Resting);
    }

    public override void OnExit()
    {

    }
}
