using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDisplay : MonoBehaviour
{
    [SerializeField] private Image m_energyBar;

    public void UpdateEnergyBar(float value, float maxValue)
    {
        m_energyBar.fillAmount = value / maxValue;
    }
}
