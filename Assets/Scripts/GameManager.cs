using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameRules GlobalGameRules;

    [Header("Opponent Data")]
    public Transform[] OpponentStructures;
    public List<Transform> OpponentUnits;

    [Header("Player Data")]
    [SerializeField] private PlayerController m_playerController;

    public Transform[] PlayerStructures;
    public List<Transform> PlayerUnits;

    public Action<float> OnEnergyUpdate;
    [Serializable]
    public struct GameRules
    {
        public float BuildingsMaxHealth;
        public float EnergyRegen;
        public float MaxEnergy;
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        // Initialize Players
        m_playerController.InitializePlayer(GlobalGameRules.MaxEnergy);
    }

    private void Update()
    {
        OnEnergyUpdate?.Invoke(GlobalGameRules.EnergyRegen);
    }
    public void OnUnitKilled(Transform unit, Team team)
    {
        List<Transform> unitsList = team == Team.Player ? PlayerUnits : OpponentUnits;
        unitsList.Remove(unit);
    }

    #region Utility

    public UnitInfo GetClosestTarget(TargetTypes targetType, Vector2 unitPosition, Team team)
    {
        List<Transform> possibleTargets = GetUnitsFromTeam(targetType, team);

        return GetClosestObject(possibleTargets, unitPosition);
    }

    private List<Transform> GetUnitsFromTeam(TargetTypes targetType, Team team)
    {
        List<Transform> result = new List<Transform>();

        if (targetType == TargetTypes.UnitsOnly || targetType == TargetTypes.Both)
        {
            foreach (var unit in team == Team.Player ? OpponentUnits : PlayerUnits)
            {
                result.Add(unit);
            }
        }

        if (targetType == TargetTypes.BuildingsOnly || targetType == TargetTypes.Both)
        {
            foreach (var building in team == Team.Player ? OpponentStructures : PlayerStructures)
            {
                // TODO: if building is already destroyed, ignore it
                result.Add(building);
            }
        }

        return result;
    }

    public UnitInfo GetClosestObject(List<Transform> objects, Vector2 unitPosition)
    {
        UnitInfo ret = null;
        float minDistance = Mathf.Infinity;
        foreach (var obj in objects)
        {
            float dist = Vector2.Distance(obj.transform.position, unitPosition);

            if (dist < minDistance)
            {
                if (obj.TryGetComponent(out UnitInfo unit))
                {
                    ret = unit;
                    minDistance = dist;
                }
            }
        }

        return ret;
    }

    //public Transform GetClosestUnit(Vector2 unitPosition, Team unitTeam)
    //{
    //    Transform ret = null;
    //    // tmp
    //    Transform[] units = unitTeam == Team.Player ? OpponentUnits.ToArray() : PlayerUnits.ToArray();

    //    float minDistance = Mathf.Infinity;
    //    foreach (var structure in units)
    //    {
    //        float dist = Vector2.Distance(structure.transform.position, unitPosition);

    //        if (dist < minDistance)
    //        {
    //            ret = structure.transform;
    //            minDistance = dist;
    //        }
    //    }

    //    return ret;
    //}

    //public Transform GetClosestStructure(Vector2 unitPosition, Team unitTeam)
    //{
    //    Transform ret = null;

    //    Transform[] structures = unitTeam == Team.Player ? OpponentStructures : PlayerStructures;

    //    float minDistance = Mathf.Infinity;
    //    foreach (var structure in structures)
    //    {
    //        float dist = Vector2.Distance(structure.transform.position, unitPosition);

    //        if (dist < minDistance)
    //        {
    //            ret = structure.transform;
    //            minDistance = dist;
    //        }
    //    }

    //    return ret;
    //}
    #endregion

}
