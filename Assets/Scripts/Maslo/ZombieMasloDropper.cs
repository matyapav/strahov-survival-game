using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMasloDropper : MonoBehaviour {

    public GameObject masloPrefab;
    public float SpawnChance = 0.25f;

    private void OnEnable()
    {
        GetComponent<ZombieStateMachine>().OnDyingStart.AddListener(SpawnOnDeath);
    }

    void SpawnOnDeath() {
        if (Random.Range(0f, 1f) < SpawnChance) {
            Instantiate(masloPrefab, new Vector3(transform.position.x, 6f, transform.position.z), Quaternion.identity);
        }
    }
}
