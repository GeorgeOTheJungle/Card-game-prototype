using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreatorController : MonoBehaviour
{
    public static UnitCreatorController Instance;
    [SerializeField] private UnitInfo m_unitPrefab;

    [Space]
    [SerializeField] private Transform m_playerUnitsRoot;
    [SerializeField] private Transform m_opponentUnitsRoot;

    private void Awake()
    {
        Instance = this;
    }
    public void CreateUnit(Vector2 position, CardData data, Team team)
    {
        // If its a spell, then instantiate the spell prefab not the unit base
        var root = team == Team.Player ? m_playerUnitsRoot : m_opponentUnitsRoot;
        var unit = Instantiate(m_unitPrefab, position, Quaternion.identity, root);
        unit.IntializeUnit(data.UnitData, team, data.CardImage, data.PreviewSprite, data.UnitAnimator);
        unit.gameObject.name = $"({team}) {data.CardName}";
    }
}
