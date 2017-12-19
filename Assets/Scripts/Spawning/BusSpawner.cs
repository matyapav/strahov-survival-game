using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusSpawner : MonoBehaviour {

    public string busName;
    public GameObject[] ZombiePrefabs;
    public int numberOfZombies = 10;
    private Animator animator;
    public float timeBetweenSpawns = 0.75f;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Arrive()
    {
        MainUISoundManager.Instance.PlaySound("bus_ride");
        animator.SetBool(animator.GetParameter(0).name, true);
        MessageController.Instance.AddMessage("Prepare yourself! Bus n. "+ busName +" is arriving!!!", 6f, Color.green);
    }

    public void StopBusRide()
    {
        MainUISoundManager.Instance.StopSound("bus_ride");
    }

    public void PlayArrivalSound()
    {
        MainUISoundManager.Instance.PlaySound("bus_arrival_sound");
    }

    public void PlayDoorSound()
    {
        MainUISoundManager.Instance.PlaySound("bus_door");
    }


    public void SpawnWave () {
        // Play the sound
        MainUISoundManager.Instance.PlaySound("spawn_horde");

        // Invoke the Spawning of the zombie
        // TODO: Add wave id info 
        StartCoroutine(SpawnZombies(numberOfZombies, timeBetweenSpawns, -1));
    }

    // Spawn a zombie
    IEnumerator SpawnZombies(int number, float waittime, int wave_id)
    {
        for (int i = 0; i < number; i++) {
            // Get a random target
            GameObject random_block = MainObjectManager.Instance.GetRandomActiveBlock();

            if (random_block == null)
            {
                Debug.LogError("There is no active block left in the scene.");
                break;
            }

            Transform target = random_block.transform;

            // Instantiate the zombie - random choose from zombie prefabs on
            int index = UnityEngine.Random.Range(0, ZombiePrefabs.Length);
            GameObject zombie = (GameObject)Instantiate(ZombiePrefabs[index], transform.position, Quaternion.Euler(0, 180, 0));

            // Update the WaveBar
            WavesController.Instance.IncreaseWaveCountAndHp(zombie.GetComponent<ZombieHealth>().health);

            // Invoke the zombie OnSpawn event
            MainEventManager.Instance.OnZombieSpawn.Invoke(zombie);

            // Add every zombie to the ObjectManager for further management
            // TODO: Set the correct wave
            MainObjectManager.Instance.AddZombieToWave(zombie, -1);

            // Set the destination of the zombie to a random target
            var zsm = zombie.GetComponent<ZombieStateMachine>();
            zsm.State = ZombieStateMachine.ZombieStateEnum.SeekPath;
            zsm.OnSeekPath.Invoke();

            // This should be done automatically 
            // zombie.GetComponent<NavMeshAgent>().SetDestination(target.position);

            yield return new WaitForSeconds(waittime);
        }

        MainUISoundManager.Instance.PlaySound("bus_ride");
        // Start the animation
        animator.SetBool(animator.GetParameter(0).name, false);

        // Invoke the event in the EventManager
        MainEventManager.Instance.OnBusLeaving.Invoke();
    }
}
