using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Card[] m_cardsInHand;

    [SerializeField] private Vector2[] m_cardTargets;

    //InitializeCards
    public void InitializeHand(List<CardData> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            m_cardsInHand[i].InitializeCard(this, cards[i], i);
        }
    }

    public void OnCardUsed(CardData cardUsed, int index)
    {
        m_cardsInHand[index].RefreshCard(PlayerController.Instance.GetNextCard(cardUsed));
    }

    public CardData GetRandomCard()
    {
        return m_cardsInHand[Random.Range(0, m_cardsInHand.Length)].GetCard();
    }
}
