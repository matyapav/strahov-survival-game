using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieStateMachine))]
public class ZombieHealth : MonoBehaviour, IDamageable<float> {

    private ZombieStateMachine zombieStateMachine;
    private AudioSource deadSound;

    public AudioClip dead1;
    public AudioClip dead2;
    public AudioClip dead3;
    public AudioClip dead4;
    public AudioClip dead5;

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
        deadSound = GetComponent<AudioSource>();
        zombieStateMachine.OnDyingStart.AddListener(Die);
    }

    public void Damage(float damage) {
        // Do nothing if already dead
        if (zombieStateMachine.IsDying()) {
            return;
        }

        if (health - damage <= 0) {
            if (!Dead()){
                zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.Dying;
            }
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
        selectDeadSound();
        deadSound.Play();
        // Remove the zombie from the MainGameObjectManager 
        MainObjectManager.Instance.RemoveZombie(gameObject);

        // Destroy itself after 2s
        Destroy(gameObject, 2f);

        // Update the WaveController
        WavesController.Instance.DecreaseWaveHealthAndCount(health);
    }

    private void selectDeadSound(){
        int randomInt = Random.Range(0, 4);

        switch (randomInt){
            case 0:
                deadSound.clip = dead1;
                break;
            case 1:
                deadSound.clip = dead2;
                break;
            case 2:
                deadSound.clip = dead3;
                break;
            case 3:
                deadSound.clip = dead4;
                break;
            case 4:
                deadSound.clip = dead5;
                break;
        }
    }
}
