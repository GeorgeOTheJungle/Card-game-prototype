using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    [SerializeField] private bool m_destroyOnKill;
    public override void OnStart()
    {
        // Play Sound
        if (m_destroyOnKill)
            Destroy(UnitStateMachine.gameObject);
        else 
            UnitStateMachine.gameObject.SetActive(false);
    }
}
