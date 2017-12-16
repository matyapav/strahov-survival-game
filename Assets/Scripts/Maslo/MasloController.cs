using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasloController : MonoBehaviour {

    public float minValue = 10f;
    public float maxValue = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Destroy(gameObject);
            CurrencyController.Instance.IncreaseValue(UnityEngine.Random.Range(minValue, maxValue));
        }
    }
}
