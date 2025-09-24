using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private EnergyDisplay m_display;

    [SerializeField] private float m_maxEnergy;
    [SerializeField] private float m_currentEnergy;



    [Header("Player Deck")]
    [Space]
    [SerializeField] private int m_currentCardDrawn;
    [SerializeField] private Deck m_currentDeck; // On game start, grab choosen deck and from that point on, that will be the one we edit.
    // Card Displays will grab info from card info and units from cardInfo.UnitInfo
    public HandManager HandManager;

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

        HandManager.InitializeHand(m_currentDeck.GetCards(4));
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

        //m_display.UpdateEnergyBar(m_currentEnergy, m_maxEnergy);
    }

    public CardData GetNextCard(CardData oldCard)
    {
        oldCard.InHand = false;
        m_currentDeck.AddCard(oldCard);
        CardData nextCard;
        nextCard = m_currentDeck.GetCard();
        //do
        //{
        //    m_currentCardDrawn++;
        //    nextCard = m_currentDeck.AvailableCards[m_currentCardDrawn];
        //} while (m_currentDeck.AvailableCards[m_currentCardDrawn].InHand);

        nextCard.InHand = true;
        return nextCard;
    }
}
