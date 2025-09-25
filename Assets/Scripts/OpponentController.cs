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
        UnitCreatorController.Instance.CreateUnit(position, PlayerController.Instance.HandManager.GetRandomCard(), Team.Opponent);
    }
}
