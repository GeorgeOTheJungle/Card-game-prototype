using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Bounds[] m_playerUnitsPlacingBounds;

    [Serializable]
    public struct Bound
    {
        public Vector2 Offset;
        public Vector2 Size;
    }

    private void OnDrawGizmosSelected()
    {
        if (m_playerUnitsPlacingBounds == null || m_playerUnitsPlacingBounds.Length == 0)
            return;

        Gizmos.color = Color.blue;
        foreach(var bound in m_playerUnitsPlacingBounds)
        {
            Gizmos.DrawWireCube(bound.center, bound.size);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    public bool ValidatePlacingPosition(Vector2 position, Team team)
    {
        if (team == Team.Player)
        {
            foreach (var bound in m_playerUnitsPlacingBounds)
            {
                if (bound.Contains(position))
                    return true;
            }
        }

        return false;
    }
}
