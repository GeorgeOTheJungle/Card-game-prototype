using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public EnergyDisplay m_display;

    [SerializeField] private float m_maxEnergy;
    [SerializeField] private float m_currentEnergy;

    

    [Header("Player Deck")]
    [Space]
    [SerializeField] private Deck m_currentDeck; // On game start, grab choosen deck and from that point on, that will be the one we edit.
    // Card Displays will grab info from card info and units from cardInfo.UnitInfo

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        var gameDeck = GameManager.Instance.DeckConfig.GetDeck();
        SetPlayerDeck(gameDeck);
    }

    public void SetPlayerDeck(Deck newDeck)
    {
        m_currentDeck = newDeck;
        m_currentDeck.ShuffleDeck();
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

    public void DeployUnit(UnitInfo unit, Vector2 position)
    {
        //var unit = Instantiate(GameManager.Instance.UnitPrefab, position, Quaternion.identity);
        //unit.IntializeUnit(CardData.UnitData, team, CardData.PreviewSprite, CardData.UnitAnimator);
    }
}
