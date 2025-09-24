using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_cardTextMesh;
    [SerializeField] private TextMeshPro m_cardCostTextMesh;

    public void Populate(string cardName, int cardCost)
    {
        m_cardTextMesh.SetText(cardName);
        m_cardCostTextMesh.SetText(cardCost.ToString());
    }
}
