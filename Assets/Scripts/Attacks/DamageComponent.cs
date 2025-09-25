using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageComponent : MonoBehaviour
{

    public virtual void Initialize() { }

    public void PerformAttack(float damage, Transform target, Vector2 center)
    {
        if (target.TryGetComponent(out HealthComponent healthComponent))
        {
            healthComponent.Damage(damage);
        }

        PerformVisualAttack(damage, target.position, center);
    }
    public abstract void PerformVisualAttack(float damage, Vector2 target, Vector2 center);
}
