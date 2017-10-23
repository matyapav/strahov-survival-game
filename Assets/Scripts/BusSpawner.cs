using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour {

    public GameObject ZombiePrefab;

    public void SpawnVave (int _numberOfZombies) {
        for (int i = 0; i < _numberOfZombies; i++) {
            Instantiate(ZombiePrefab, transform.position, Quaternion.identity);
        }
    }
}
