using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour
{
    public UnitInfo Target;
    [SerializeField] private UnitStates m_startingState;
    [SerializeField] private UnitStates m_currentState;
    [HideInInspector] public UnitInfo m_unitInfo;

    [SerializeField] private StateData[] m_states;

    private State m_state;
    private HealthComponent m_healthComponent;

    private void Awake()
    {
        m_unitInfo = GetComponent<UnitInfo>();
        m_healthComponent = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        InitializeStates();

        m_healthComponent.InitializeHealth(m_unitInfo.UnitData.Health, this);
    }

    public void DeployUnit()
    {

    }
    private void InitializeStates()
    {
        foreach (var state in m_states)
        {
            state.State.Initialize(m_unitInfo, this);
        }

        ChangeState(m_startingState);
    }

    private void Update()
    {
        if (m_state == null)
            return;

        m_state.OnUpdate();
    }

    public void ChangeState(UnitStates nextState)
    {
        if (m_state)
            m_state.OnExit();

        foreach(var state in m_states)
        {
            if (state.UnitState != nextState)
                continue;

            m_state = state.State;
        }

        m_currentState = nextState;
        m_state.OnStart();
    }

    public void FindTarget()
    {
        if (Target != null)
            return;

        Target = GameManager.Instance.GetClosestTarget(m_unitInfo.UnitData.Targets, transform.position, m_unitInfo.Team);
    }
}

[Serializable]
public struct StateData
{
    public UnitStates UnitState;
    public State State;
}

public enum UnitStates
{
    Invoking,
    Idle,
    Moving,
    Channeling,
    Attacking,
    Resting,
    Dead,
}




