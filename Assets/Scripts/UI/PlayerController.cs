using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EnergyDisplay m_display;

    [SerializeField] private float m_maxEnergy;
    [SerializeField] private float m_currentEnergy;

    [Header("Player Deck")]
    [Space]
    [SerializeField] private Deck m_currentDeck;

    [Serializable]
    public class Deck
    {
        public List<CardScriptable> AvailableCards;

        public void ShuffleDeck()
        {
            System.Random rng = new System.Random();
            AvailableCards = AvailableCards.OrderBy(_ => rng.Next()).ToList();
        }
    }

    public void InitializePlayer(float maxEnergy)
    {
        m_maxEnergy = maxEnergy;

        GameManager.Instance.OnEnergyUpdate -= UpdateEnergy;
        GameManager.Instance.OnEnergyUpdate += UpdateEnergy;

        m_currentDeck.ShuffleDeck();
    }

    private void UpdateEnergy(float regenRate)
    {
        if (m_currentEnergy < m_maxEnergy)
        {
            m_currentEnergy += Time.deltaTime * regenRate;
        }
        else
        {
            m_currentEnergy = m_maxEnergy;
        }

        m_display.UpdateEnergyBar(m_currentEnergy, m_maxEnergy);
    }
}
