using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCS : MonoBehaviour
{
    // On enable add the listener to "subscribe"
    void OnEnable()
    {
        Player.onEnemyHit += Damage;
    }

    // On disable unsibscribe
    void OnDisable()
    {
        Player.onEnemyHit -= Damage;
    }

    void Damage(Color _newColor)
    {
        GetComponent<Renderer>().material.color = _newColor;
    }
}