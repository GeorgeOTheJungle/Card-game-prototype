using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new unit", menuName = "Card")]
public class CardScriptable : ScriptableObject
{
    public CardData CardData;

    public void OnValidate()
    {
        CardData.UnitData.RangedAttacker = CardData.UnitData.Range > 1.0f;
    }
}
