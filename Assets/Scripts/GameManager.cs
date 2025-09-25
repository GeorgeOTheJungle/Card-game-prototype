using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardData;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameRules GlobalGameRules;

    [Header("Opponent Data")]
    public List<TowerComponent> OpponentStructures;
    public List<Transform> OpponentUnits;

    [Header("Player Data")]
    [SerializeField] private PlayerController m_playerController;

    public List<TowerComponent> PlayerStructures;
    public List<Transform> PlayerUnits;

    [Header("Global References"), Space(5)]
    public DeckConfig DeckConfig;

    public Material RangeAttackMaterial;

    

    [Space(5)]    
    public int LightUnitHealthPerLevel;
    public int MediumUnitHealthPerLevel;
    public int HeavyUnitHealthPerLevel;

    [Space(5)]
    public int LightUnitDamagePerLevel;
    public int MediumUnitDamagePerLevel;
    public int HeavyUnitDamagePerLevel;

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
        foreach (var tower in OpponentStructures)
        {
            tower.InitializeTower();
        }

        foreach (var tower in PlayerStructures)
        {
            tower.InitializeTower();
        }
    }

    private void Update()
    {
        OnEnergyUpdate?.Invoke(GlobalGameRules.EnergyRegen);
    }
    public void RemoveUnitFromTeamList(Transform unit, Team team)
    {
        List<Transform> unitsList = team == Team.Player ? PlayerUnits : OpponentUnits;
        unitsList.Remove(unit);
    }

    public void RemoveTowerFromTeamList(TowerComponent tower, Team team)
    {
        List<TowerComponent> towerList = team == Team.Player ? PlayerStructures : OpponentStructures;
        towerList.Remove(tower);
    }

    public void AddUnitToTeamList(Transform unit, Team team)
    {
        List<Transform> unitsList = team == Team.Player ? PlayerUnits : OpponentUnits;
        unitsList.Add(unit);
    }

    #region Utility

    public int GetHealthPerLevel(CardTypes cardType)
    {
        return cardType switch
        {
            CardTypes.Unit_Light => LightUnitHealthPerLevel,
            CardTypes.Unit_Medium => MediumUnitHealthPerLevel,
            CardTypes.Unit_Heavy => HeavyUnitHealthPerLevel,
            _ => 0,
        };
    }

    public int GetDamagePerLevel(CardTypes cardType)
    {
        return cardType switch
        {
            CardTypes.Unit_Light => LightUnitDamagePerLevel,
            CardTypes.Unit_Medium => MediumUnitDamagePerLevel,
            CardTypes.Unit_Heavy => HeavyUnitDamagePerLevel,
            _ => 0,
        };
    }

    public UnitStateMachine.Unit GetClosestTarget(TargetTypes targetType, Vector2 unitPosition, Team team)
    {
        List<Transform> possibleTargets = GetUnitsFromTeam(targetType, team);

        return GetClosestObject(possibleTargets, unitPosition);
    }

    private List<Transform> GetUnitsFromTeam(TargetTypes targetType, Team team)
    {
        List<Transform> result = new List<Transform>();
        // IF the unit target units but there is none, by default it should 
        switch (targetType)
        {
            case TargetTypes.Both:
                foreach (var unit in team == Team.Player ? OpponentUnits : PlayerUnits)
                {
                    result.Add(unit);
                }

                foreach (var building in team == Team.Player ? OpponentStructures : PlayerStructures)
                {
                    result.Add(building.transform);
                }
                break;
            case TargetTypes.UnitsOnly:
                foreach (var unit in team == Team.Player ? OpponentUnits : PlayerUnits)
                {
                    result.Add(unit);
                }
                break;
            case TargetTypes.BuildingsOnly:
                foreach (var building in team == Team.Player ? OpponentStructures : PlayerStructures)
                {
                    // TODO: if building is already destroyed, ignore it
                    result.Add(building.transform);
                }
                break;
        }
        return result;
    }

    public UnitStateMachine.Unit GetClosestObject(List<Transform> objects, Vector2 unitPosition)
    {
        UnitStateMachine.Unit ret = new();
        float minDistance = Mathf.Infinity;
        foreach (var obj in objects)
        {
            float dist = Vector2.Distance(obj.transform.position, unitPosition);

            if (dist < minDistance)
            {
                if (obj.TryGetComponent(out UnitInfo unit))
                {
                    ret.Info = unit;
                    ret.TowerComponent = null;
                    ret.UnitTransform = unit.transform;

                    minDistance = dist;
                }

                if (obj.TryGetComponent(out TowerComponent tower))
                {
                    ret.Info = null;
                    ret.TowerComponent = tower;
                    ret.UnitTransform = tower.transform;

                    minDistance = dist;
                }
            }
        }

        return ret;
    }

    #endregion

    #region Debug

    private bool m_buildingsAttack = true;
    public void ToggleBuildingsAttack()
    {
        m_buildingsAttack = !m_buildingsAttack;

        foreach(var building in OpponentStructures)
        {
            building.ToggleTowerRange();
        }

        foreach (var building in PlayerStructures)
        {
            building.ToggleTowerRange();
        }
    }
    #endregion
}
