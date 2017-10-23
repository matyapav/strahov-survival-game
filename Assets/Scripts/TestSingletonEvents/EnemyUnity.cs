using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnity : MonoBehaviour
{

    void OnEnable()
    {
        Player.Instance.m_MyEvent.AddListener(Damage);
    }

    void OnDisable()
    {
        // Must be checked while destroying since the Singleton can be destroying atm
        if (Player.Instance != null)
        {
            Player.Instance.m_MyEvent.RemoveListener(Damage);
        }
    }

    void Damage(Color _newColor)
    {
        GetComponent<Renderer>().material.color = _newColor;
    }
}