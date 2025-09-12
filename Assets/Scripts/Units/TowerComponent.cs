using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerComponent : MonoBehaviour
{
    private UnitInfo m_unitInfo;
    [SerializeField] private HealthComponent m_healthComponent;

    private void Awake()
    {
        m_unitInfo = GetComponent<UnitInfo>();
        m_healthComponent = GetComponent<HealthComponent>();
    }


}
