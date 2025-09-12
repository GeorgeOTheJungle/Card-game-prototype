using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamComponent : MonoBehaviour
{
    public int TeamIndex;

    public bool IsOpponent(int unitTeamIndex)
    {
        return TeamIndex != unitTeamIndex;
    }
}
