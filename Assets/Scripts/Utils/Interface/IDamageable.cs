using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damageable.
/// </summary>
/// <remarks>
/// Unifies all damageable objects in the scene.
/// </remarks>
public interface IDamageable<T> {
    void Damage(T damage);
    bool Dead();
}
