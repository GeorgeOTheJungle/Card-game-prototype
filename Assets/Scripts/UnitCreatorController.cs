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
        
        var unit = Instantiate(m_unitPrefab, position, Quaternion.identity);
        unit.IntializeUnit(data.UnitData, team, data.CardImage, data.PreviewSprite, data.UnitAnimator);

    }
}
