using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieStateMachine))]
public class ZombieHealth : MonoBehaviour, IDamageable<float> {

    private ZombieStateMachine zombieStateMachine;

    public float health = 100;

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
        // TODO spatne funguje - obcas umre zombie treba 5x = zasekla animace a funkce Die se provola mnohonasobne
        // Snad jsem to opravil (@Matyáš)
        if(health - damage <= 0){
            if (!zombieStateMachine.IsDying())
            {
                health = 0f;
                zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.Dying;
            }
        }
        else {
            health -= damage;
            WavesController.Instance.DecreaseWaveHealth(damage);
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

        // Update the WaveController
        WavesController.Instance.DecreaseWaveHealth(health);
        WavesController.Instance.DecreaseWaveCount(1);
    }
}
