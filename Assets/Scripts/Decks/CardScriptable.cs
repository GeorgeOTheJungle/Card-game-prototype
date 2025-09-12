using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptable : ScriptableObject
{
    public string CardName;
    public TargetTypes Target;
    [Space]

    public float Health;
    public float Range;
    [Space]

    public float Damage;
    public float Speed;

    [Space]
    public float InvokeTime;
    public float ChannelSpeed;
    public float RestTime;

    [Space]
    public Sprite PreviewSprite;
    public Animator UnitAnimator;
}
