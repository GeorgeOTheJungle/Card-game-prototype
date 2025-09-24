using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public static OpponentController Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void PlaceUnit()
    {
        PlaceUnit(BoardManager.Instance.GetPlacingPosition(Team.Opponent));
    }

    public void PlaceUnit(Vector2 position)
    {
        Debug.Log("Opponent Card dropped");

        //var prefab = GameManager.Instance.UnitPrefab;
        //var unit = Instantiate(prefab, forcedPosition ?? transform.position, Quaternion.identity);
        //unit.IntializeUnit(m_data.UnitData, Team.Player, m_data.CardImage, m_data.PreviewSprite, m_data.UnitAnimator);

        UnitCreatorController.Instance.CreateUnit(position, PlayerController.Instance.HandManager.GetRandomCard(), Team.Opponent);

        //PreviewComponent.Instance.SetPreview();
        /////m_cardVisuals.SetActive(true);

        //m_handManager.OnCardUsed(m_data, m_cardIndex);
    }
}
