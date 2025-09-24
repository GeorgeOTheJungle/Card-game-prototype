using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Bounds[] m_playerUnitsPlacingBounds;

    [SerializeField] private Bounds[] m_opponentUnitsPlacingBounds;

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

        Gizmos.color = Color.red;
        foreach (var bound in m_opponentUnitsPlacingBounds)
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
        var bounds = team == Team.Player ? m_playerUnitsPlacingBounds : m_opponentUnitsPlacingBounds;
        foreach (var bound in bounds)
        {
            if (bound.Contains(position))
                return true;
        }

        return false;
    }

    public Vector2 GetPlacingPosition(Team team)
    {
        var bounds = team == Team.Player ? m_playerUnitsPlacingBounds : m_opponentUnitsPlacingBounds;
        var bound = bounds[UnityEngine.Random.Range(0, bounds.Length)];

        float boundX = UnityEngine.Random.Range(bound.min.x, bound.max.x);
        float boundY = UnityEngine.Random.Range(bound.min.y, bound.max.y);
        Vector2 position = new Vector2(boundX, boundY);

        return position;
    }
}
