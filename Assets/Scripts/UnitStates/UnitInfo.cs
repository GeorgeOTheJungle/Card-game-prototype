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

    public void SetUnitStats(UnitData data)
    {
        UnitData = data;
    }
}

[Serializable]
public struct UnitData
{
    public string Name;
    public TargetTypes Target;
    [Space]

    public float Health;
    public float Damage;
    public float ChannelSpeed;
    [Space]

    public float Range;
    public float Speed;
    [Space]

    public float RestTime;
    public float InvokeTime;
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
