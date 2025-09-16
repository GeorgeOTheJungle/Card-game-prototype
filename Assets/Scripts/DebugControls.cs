using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    [SerializeField] private GameObject m_dummyPointer;
    [SerializeField] private CardDisplay[] m_cards;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            bool canPlaceUnit = BoardManager.Instance.ValidatePlacingPosition(m_dummyPointer.transform.position, Team.Player);
            if (canPlaceUnit)
            {
                m_cards[0].OnCardPlaced();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Place unit in a random position
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Place unit in a random position
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // Place unit in a random position
        }
    }
}
