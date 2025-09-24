using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokingState : State
{
    [SerializeField] private float m_invokeCounter;

    [SerializeField] private GameObject m_previewSpriteRenderer;
    [SerializeField] private GameObject m_activeSpriteRenderer;

    public override void OnIntialization()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }
    public override void OnStart()
    {
        m_invokeCounter = UnitInfo.UnitData.InvokeTime;

        m_previewSpriteRenderer.SetActive(true);
        m_activeSpriteRenderer.SetActive(false);
    }

    public override void OnUpdate()
    {
        if (m_invokeCounter > 0.0f)
        {
            m_invokeCounter -= Time.deltaTime;
        }
        else
        {
            UnitStateMachine.FindTarget();
            UnitStateMachine.ChangeState(UnitStates.Moving);

            m_previewSpriteRenderer.SetActive(false);
            m_activeSpriteRenderer.SetActive(true);
        }
    }
}
