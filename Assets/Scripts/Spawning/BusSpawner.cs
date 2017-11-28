using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusSpawner : MonoBehaviour {

    public string busName;
    public GameObject ZombiePrefab;
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

    public void Leave()
    {
        MainUISoundManager.Instance.PlaySound("bus_ride");
        // Start the animation
        animator.SetBool(animator.GetParameter(0).name, false);

        // Invoke the event in the EventManager
        MainEventManager.Instance.OnBusLeaving.Invoke();
    }

    public void SpawnWave () {
        MainUISoundManager.Instance.PlaySound("spawn_horde");
        for (int i = 0; i < numberOfZombies; i++) {
            Invoke("SpawnZombie", i* timeBetweenSpawns);
        }
        Invoke("Leave", (numberOfZombies + 1) * timeBetweenSpawns);
    }

    // Spawn a zombie
    private void SpawnZombie()
    {
        // Get a random target
        Transform target = MainObjectManager.Instance.GetRandomBlock().transform;

        // Instantiate the zombie
        GameObject zombie = (GameObject) Instantiate(ZombiePrefab, transform.position, Quaternion.Euler(0, 180, 0));

        // Invoke the zombie OnSpawn event
        MainEventManager.Instance.OnZombieSpawn.Invoke(zombie);

        // Add every zombie to the ObjectManager for further management
        MainObjectManager.Instance.zombies.Add(zombie);

        // Set the destination of the zombie to a random target
		zombie.GetComponent<NavMeshAgent>().SetDestination(target.position);
    }
}
