using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public Team Team;
    public UnitData UnitData;
    public Transform UnitCenter;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(UnitCenter.position, UnitData.Range);
    }

    public void IntializeUnit(UnitData card, Team team, Sprite preview, Animator animator)
    {
        Team = team;

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
    UnitsOnly,
    BuildingsOnly,
    Both,
}
