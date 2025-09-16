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
    public Transform[] OpponentStructures;
    public List<Transform> OpponentUnits;

    [Header("Player Data")]
    [SerializeField] private PlayerController m_playerController;

    public Transform[] PlayerStructures;
    public List<Transform> PlayerUnits;

    [Header("Global References"), Space(5)]
    public DeckConfig DeckConfig;
    public UnitInfo UnitPrefab;


    

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

    public void AddUnitToTeamList(Transform unit, Team team)
    {
        List<Transform> unitsList = team == Team.Player ? PlayerUnits : OpponentUnits;
        unitsList.Add(unit);
    }

    public void DeployUnitToBoard(UnitInfo unit, Vector2 position, Team team)
    {

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

    #endregion

}
