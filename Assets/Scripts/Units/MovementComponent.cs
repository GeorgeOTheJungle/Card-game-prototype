using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovementComponent : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    private NavMeshAgent m_agent;


    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.updateRotation = false;
        m_agent.updateUpAxis = false;
    }

    private void Update()
    {
        
    }

    public void SetUnitTarget(Transform target)
    {
        m_target = target;
    }
}
