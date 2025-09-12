using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageComponent : MonoBehaviour
{
    public void PerformAttack(float damage, UnitInfo targetInfo, Vector2 center)
    {
        if (targetInfo.TryGetComponent(out HealthComponent healthComponent))
        {
            healthComponent.Damage(damage);
        }

        PerformVisualAttack(damage, targetInfo.UnitCenter.position, center);
    }
    public abstract void PerformVisualAttack(float damage, Vector2 target, Vector2 center);
}
