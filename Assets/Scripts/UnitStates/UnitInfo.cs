using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public Team Team;
    public UnitData UnitData;
    public Transform UnitCenter;

    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private SpriteRenderer m_previewRendere;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(UnitCenter.position, UnitData.Range);
    }

    public void IntializeUnit(UnitData data, Team team, Sprite activeSprite, Sprite preview, Animator animator)
    {
        Team = team;

        UnitData = data;

        m_spriteRenderer.sprite = activeSprite;
        m_previewRendere.sprite = preview;

        //Animator

        GameManager.Instance.AddUnitToTeamList(transform, team);
    }
}



public enum Team
{
    Player,
    Opponent
}

public enum TargetTypes
{
    UnitsOnly, //For buildings
    BuildingsOnly,
    Both,
}
