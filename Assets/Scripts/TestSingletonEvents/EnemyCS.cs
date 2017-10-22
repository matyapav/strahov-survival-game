using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCS : MonoBehaviour
{

    void OnEnable()
    {
        Player.onEnemyHit += Damage;
    }

    void OnDestroy()
    {
        Player.onEnemyHit -= Damage;
    }

    void Damage(Color _newColor)
    {
        GetComponent<Renderer>().material.color = _newColor;
    }
}