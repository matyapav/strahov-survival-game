using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieStateMachine))]
public class ZombieHealth : MonoBehaviour, IDamageable<float> {

    private ZombieStateMachine zombieStateMachine;

    public float health = 100;

    private bool _dead = false;
    public bool dead {
        get { 
            return _dead; 
        }
    }

    private float _startingHealth;
    public float GetStartingHealth() {
        return _startingHealth;
    }

    // Add the listener
    private void OnEnable()
    {
        _startingHealth = health;

        zombieStateMachine = GetComponent<ZombieStateMachine>();

        zombieStateMachine.OnDying.AddListener(Die);
    }

    public void Damage(float damage) {
        health -= damage;
        if (health <= 0)
        {
            if (!zombieStateMachine.IsDying())
            {
                zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.Dying;
            }
        }
    }

    public bool Dead() {
        return health <= 0;
    }

    // Do some stuff to dispose itself
    void Die () {
        // Remove the zombie from the MainGameObjectManager list
        if (MainObjectManager.Instance.zombies.Contains(gameObject)) {
            MainObjectManager.Instance.zombies.Remove(gameObject);
        }

        // Destroy itself after 2s
        Destroy(this.gameObject, 2f);
    }
}
