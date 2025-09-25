using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerComponent : MonoBehaviour
{
    [SerializeField] private TowerState m_towerState;
    [SerializeField] private TowerData m_towerData;

    private HealthComponent m_healthComponent;
    private ProjectileAttack m_projectileAttack;

    [SerializeField] private Transform m_target;

    private float m_attackSpeedCounter;

    [Serializable]
    public struct TowerData
    {
        public Team Team;
        public int Health;

        public float IdleTime;

        public int Damage;
        public float AttackSpeed;
        public float Range;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_target ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_towerData.Range);
    }
    private void Awake()
    {
        m_healthComponent = GetComponent<HealthComponent>();
        m_projectileAttack = GetComponent<ProjectileAttack>();
        m_projectileAttack.Initialize();
    }

    public void InitializeTower()
    {
        m_attackSpeedCounter = m_towerData.AttackSpeed;

        m_healthComponent.InitializeHealth(m_towerData.Health, this);
    }

    private void Update()
    {
        if (m_healthComponent.Alive == false)
            return;

        TowerBehavior();
    }

    private void TowerBehavior()
    {
        switch(m_towerState)
        {
            case TowerState.Idle:
                var layerMask = LayerMask.GetMask("Unit");
                var targetCheck = Physics2D.OverlapCircleAll(transform.position, m_towerData.Range, layerMask);

                foreach (var target in targetCheck)
                {
                    if (target.TryGetComponent(out UnitInfo unit))
                    {
                        if (unit.Team == m_towerData.Team)
                        {
                            // Ally targeting only?
                            continue;
                        }

                        m_target = target.transform;
                        m_towerState = TowerState.Channeling;
                        break;
                    }
                }
                break;

            case TowerState.Channeling:
                if (m_target == null)
                {
                    m_towerState = TowerState.Idle;
                }

                if (m_attackSpeedCounter > 0.0f)
                {
                    m_attackSpeedCounter -= Time.deltaTime;
                }
                else
                {
                    m_towerState = TowerState.Attacking;
                }

                    break;

            case TowerState.Attacking:
                m_attackSpeedCounter = m_towerData.AttackSpeed;

                Vector2 center = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
                m_projectileAttack.PerformAttack(m_towerData.Damage, m_target, center);

                m_towerState = TowerState.Channeling;
                break;

            case TowerState.Dead:
                break;
        }
    }

    public void DestroyTower()
    {
        m_towerState = TowerState.Dead;
    }

    public void ToggleTowerRange()
    {
        m_towerData.Range = 0f;
    }

    public enum TowerState
    {
        Idle,
        Channeling,
        Attacking,
        Dead
    }
}
