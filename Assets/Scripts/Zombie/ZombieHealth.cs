using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieStateMachine))]
public class ZombieHealth : MonoBehaviour, IDamageable<float> {

    private ZombieStateMachine zombieStateMachine;

    public float health = 100;

    private float _startingHealth;

    // The health that zombie had when started
    public float GetStartingHealth() {
        return _startingHealth;
    }

    private void OnEnable()
    {
        _startingHealth = health;
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        zombieStateMachine.OnDying.AddListener(Die);
    }

    public void Damage(float damage) {
        // Do nothing if already dead
        if (zombieStateMachine.IsDying()) {
            return;
        }

        if (health - damage <= 0) {
            zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.Dying;
        }
        else {
            health -= damage;
            WavesController.Instance.DecreaseWaveHealth(damage);
        }
    }

    public bool Dead () {
        return zombieStateMachine.IsDying();
    }

    private void Die () {
        // Remove the zombie from the MainGameObjectManager 
        MainObjectManager.Instance.RemoveZombie(gameObject);

        // Destroy itself after 2s
        Destroy(gameObject, 2f);

        // Update the WaveController
        WavesController.Instance.DecreaseWaveHealthAndCount(health);
    }
}
