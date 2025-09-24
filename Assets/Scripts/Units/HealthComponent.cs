using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float m_currentHealth;
    private float m_maxHealth;
    private UnitStateMachine m_stateMachine;
    private TowerComponent m_towerComponent;

    [SerializeField] private Image m_healthBar;

    public void InitializeHealth(float maxHealth, TowerComponent towerComponent)
    {
        //InitializeHealth(maxHealth, null);

        m_towerComponent = towerComponent;
    }

    public void InitializeHealth(float maxHealth, UnitStateMachine unitStateMachine)
    {
        m_maxHealth = maxHealth;
        m_currentHealth = maxHealth;

        m_stateMachine = unitStateMachine;
    }

    public void Damage(float damage)
    {
        m_currentHealth -= damage;

        if (m_currentHealth <= 0)
        {
            m_stateMachine.ChangeState(UnitStates.Dead);
            GameManager.Instance.RemoveUnitFromTeamList(transform, m_stateMachine.m_unitInfo.Team);
        }

        m_healthBar.fillAmount = m_currentHealth / m_maxHealth;
    }
}
