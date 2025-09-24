using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private int m_cardIndex;
    [SerializeField] private CardData m_data;
    [SerializeField] private GameObject m_cardVisuals;

    private bool m_insideBoard;
    private CardDisplay m_cardDisplay;
    private HandManager m_handManager;
    private void Awake()
    {
        m_cardDisplay = GetComponent<CardDisplay>();
    }
    public void RefreshCard(CardData cardData)
    {
        m_data = cardData;
        m_cardVisuals.SetActive(true);

        m_cardDisplay.Populate(m_data.CardName, m_data.CardCost);
        transform.name = m_data.CardName;
    }
    public void InitializeCard(HandManager handManager, CardData cardData, int cardIndex)
    {
        m_handManager = handManager;
        m_cardIndex = cardIndex;
        m_data = cardData;

        m_cardDisplay.Populate(m_data.CardName, m_data.CardCost);
        transform.name = m_data.CardName;
    }

    public void DragCard()
    {
        m_insideBoard = BoardManager.Instance.ValidatePlacingPosition(transform.position, Team.Player);
        m_cardVisuals.SetActive(!m_insideBoard);
        if (m_insideBoard)
        {
            Debug.Log("Inside board");
            PreviewComponent.Instance.SetPreview(transform.position, m_data.PreviewSprite);
        }
        else
        {
            Debug.Log("Outside board");
            PreviewComponent.Instance.SetPreview();
        }
    }
    public void UseCard()
    {

        if (m_insideBoard == false)
            return;
        Debug.Log("Card dropped");
        //m_data.InHand = false;
        //var prefab = GameManager.Instance.UnitPrefab;
        //var unit = Instantiate(prefab, forcedPosition ?? transform.position, Quaternion.identity);
        //unit.IntializeUnit(m_data.UnitData, Team.Player, m_data.CardImage, m_data.PreviewSprite, m_data.UnitAnimator);
        UnitCreatorController.Instance.CreateUnit(transform.position, m_data, Team.Player);
        
        PreviewComponent.Instance.SetPreview();
        ///m_cardVisuals.SetActive(true);

        m_handManager.OnCardUsed(m_data, m_cardIndex);
    }

    public CardData GetCard()
    {
        return m_data;
    }
}
