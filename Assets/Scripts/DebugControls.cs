using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControls : MonoBehaviour
{
    [SerializeField] private GameObject m_dummyPointer;
    [SerializeField] private Card[] m_cards;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OpponentController.Instance.PlaceUnit();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Toggle Building damage
            GameManager.Instance.ToggleBuildingsAttack();
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
