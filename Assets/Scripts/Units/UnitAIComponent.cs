using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main body of the unit
[RequireComponent(typeof(MovementComponent),typeof(TeamComponent),typeof(AttackComponent))]
public class UnitAIComponent : MonoBehaviour
{
    [Header("Unit Parameters")]
    [SerializeField]
    private UnitData m_unitData;

    [SerializeField]
    private bool m_isOpponent;

    private float m_invokeCounter;
    [SerializeField] private UnitStates m_unitState;

    private TeamComponent m_teamComponent;
    private MovementComponent m_movementComponent;
    private AttackComponent m_attackComponent;

    
    private State m_state;
    private void Awake()
    {
        m_movementComponent = GetComponent<MovementComponent>();
        m_teamComponent = GetComponent<TeamComponent>();
        m_attackComponent = GetComponent<AttackComponent>();
    }

    private void Start()
    {
        InvokeUnit();
    }

    private void Update()
    {
        HandleStateMachine();

        //if (m_target == null && (m_unitState != UnitStates.Invoking || m_unitState != UnitStates.Dead))
        //    CheckForTargetUnits();

        //if (m_target == null)
        //    return;

        //if (Vector2.Distance(transform.position, m_target.position) <= m_unitData.Range)
        //{
        //    m_unitState = UnitStates.Channeling;
        //}
    }

    public virtual void InvokeUnit()
    {
        m_invokeCounter = m_unitData.InvokeTime;
        OnStart();
    }

    public virtual void OnStart()
    {

    }



    private void HandleStateMachine()
    {
        switch(m_unitState)
        {
            case UnitStates.Invoking:
                if (m_invokeCounter > 0.0f)
                {
                    m_invokeCounter -= Time.deltaTime;
                } else
                {
                    m_unitState = UnitStates.Moving;
                }

                break;
            case UnitStates.Idle:

                break;
            case UnitStates.Moving:
                //if (m_target != null)
                //{
                //    m_agent.SetDestination(m_target.position);
                //}

                break;
            case UnitStates.Channeling:
                break;
            case UnitStates.Attacking:
                break;
            case UnitStates.SpecialAbility:
                break;
            case UnitStates.Dead:
                break;
        }
    }

    private void CheckForTargetUnits()
    {
        var layerMask = LayerMask.GetMask("Unit");
        var targetCheck = Physics2D.OverlapCircleAll(transform.position, m_unitData.Range, layerMask);
        Transform targetPosition = null;

        foreach (var target in targetCheck)
        {
            if (target.TryGetComponent(out TeamComponent component))
            {
                if (component.IsOpponent(m_teamComponent.TeamIndex) == false)
                    continue;

                targetPosition = target.transform;
                break;
            }
        }

        if (targetPosition == null)
        {
           // targetPosition = GameManager.Instance.GetClosestStructure(transform.position, m_teamComponent.TeamIndex);
        }

        //SetUnitTarget(targetPosition);
    }

    protected virtual void OnIdle()
    {

    }

    protected virtual void OnMoving()
    {

    }

    public enum UnitStates
    {
        Invoking,
        Idle,
        Moving,
        Channeling,
        Attacking,
        SpecialAbility,
        Dead
    }
}


