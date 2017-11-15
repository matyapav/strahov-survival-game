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
    private float timeBetweenSpawns = 0.75f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Arrive()
    {
        animator.SetBool(animator.GetParameter(0).name, true);
        MessageController.Instance.AddMessage("Prepare yourself! Bus n. "+ busName +" is arriving!!!", 6f, Color.green);
    }

    public void Leave()
    {
        // Start the animation
        animator.SetBool(animator.GetParameter(0).name, false);

        // Invoke the event in the EventManager
        MainEventManager.Instance.OnBusLeaving.Invoke();
    }

    public void SpawnWave () {
        for (int i = 0; i < numberOfZombies; i++) {
            Invoke("SpawnZombie", i* timeBetweenSpawns);
        }
        Invoke("Leave", (numberOfZombies + 1) * timeBetweenSpawns);
    }

    // Spawn a zombie
    private void SpawnZombie()
    {
        // Get a random target
        Transform target = randomTargetFromTargets();

        // Instantiate the zombie
        GameObject zombie = (GameObject) Instantiate(ZombiePrefab, transform.position, Quaternion.Euler(0, 180, 0));

        // Invoke the zombie OnSpawn event
        MainEventManager.Instance.OnZombieSpawn.Invoke(zombie);

        // Add every zombie to the ObjectManager for further management
        MainObjectManager.Instance.zombies.Add(zombie);

        // Set the destination of the zombie to a random target
		zombie.GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    // Get a random blok as target
    private Transform randomTargetFromTargets()
    {
        return MainObjectManager.Instance.bloky[UnityEngine.Random.Range(0, MainObjectManager.Instance.bloky.Length)].transform.GetChild(0);
    }
}
