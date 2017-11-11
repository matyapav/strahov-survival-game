using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesController : MonoBehaviour {

    public BusSpawner[] buses;

    private int actualBusIndex = 0;
    private bool canSpawnNewWave = true;

    private void Start()
    {
        // For each bus add a listener to the MainEventManager
        foreach (BusSpawner spawner in buses)
        {
            MainEventManager.Instance.OnBusLeaving.AddListener(AllowSpawningNewWave);
        }
    }

    public void StartNextWave()
    {
        if (canSpawnNewWave)
        {
            buses[actualBusIndex].Arrive();
            actualBusIndex = (actualBusIndex + 1) % buses.Length; //cycle buses
            canSpawnNewWave = false;
        } else
        {
            Debug.Log("Cannot spawn new wave. Previous wave is still spawning..");
        }
    }

    public void AllowSpawningNewWave() {
        canSpawnNewWave = true;
    }
}
