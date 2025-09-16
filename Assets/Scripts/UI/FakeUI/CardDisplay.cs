using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    [SerializeField]
    private CardData m_cardData;

    public void SetCardData(CardData cardData)
    {
       m_cardData = cardData;
    }

    public void OnCardPlaced()
    {

    }
}
