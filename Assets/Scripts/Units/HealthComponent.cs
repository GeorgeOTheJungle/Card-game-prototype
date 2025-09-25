using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    public bool Alive
    {
        get
        {
            return m_currentHealth > 0;
        }
    }
    [SerializeField] private float m_currentHealth;
    private float m_maxHealth;
    private UnitStateMachine m_stateMachine;
    private TowerComponent m_towerComponent;

    [SerializeField] private Image m_healthBar;

    public void InitializeHealth(float maxHealth, TowerComponent towerComponent)
    {
        m_towerComponent = towerComponent;
        InitializeHealth(maxHealth);
    }

    public void InitializeHealth(float maxHealth, UnitStateMachine unitStateMachine = null)
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
            if (m_stateMachine)
            {
                m_stateMachine.ChangeState(UnitStates.Dead);
                GameManager.Instance.RemoveUnitFromTeamList(transform, m_stateMachine.m_unitInfo.Team);
            }
            else if (m_towerComponent)
            {
                m_towerComponent.DestroyTower();
                GameManager.Instance.RemoveTowerFromTeamList(m_towerComponent, m_stateMachine.m_unitInfo.Team);
            }
        }

        m_healthBar.fillAmount = m_currentHealth / m_maxHealth;
    }
}
