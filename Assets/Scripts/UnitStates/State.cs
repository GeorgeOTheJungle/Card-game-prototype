using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    public bool IsComplete { get; protected set; }

    protected float m_startTime;

    public float RunTime => Time.time - m_startTime;

    protected UnitStateMachine UnitStateMachine;
    protected UnitInfo UnitInfo;
    protected NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponentInParent<NavMeshAgent>();
        if (Agent)
        {
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
        }
    }
    public void Initialize(UnitInfo unitInfo, UnitStateMachine unitStateMachine)
    {
        UnitStateMachine = unitStateMachine;
        UnitInfo = unitInfo;



        OnIntialization();
    }

    public virtual void OnIntialization() { }
    public virtual void OnStart() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnExit() { }



}
