using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour {

    public float health = 100;

    public void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play the dying animation and delete afterwards

        // Play the death sound
        //audio.Play();

        // Remove from the global object manager

        // Chance to drop a butter

        // temporary disable only
        gameObject.SetActive(false);
    }
}
