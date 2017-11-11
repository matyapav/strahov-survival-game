using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour {

    public int busNumber;
    public GameObject ZombiePrefab;
    public int numberOfZombies = 10;
    private Animator animator;
    private float timeBetweenSpawns = 0.75f;

    public delegate void OnBusLeaving();
    public OnBusLeaving onBusLeaving;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Arrive()
    {
        animator.SetBool(animator.GetParameter(0).name, true);
        MessageController.Instance.AddMessage("Prepare yourself! Bus n. "+ busNumber +" is arriving!!!", 6f, Color.green);
    }

    public void Leave()
    {
        animator.SetBool(animator.GetParameter(0).name, false);
        onBusLeaving.Invoke();
    }

    public void SpawnWave () {
        for (int i = 0; i < numberOfZombies; i++) {
            Invoke("SpawnZombie", i* timeBetweenSpawns);
        }
        Invoke("Leave", (numberOfZombies + 1) * timeBetweenSpawns);
    }

    private void SpawnZombie()
    {
        Transform target = randomTargetFromTargets();
        GameObject zombie = (GameObject) Instantiate(ZombiePrefab, transform.position, Quaternion.Euler(0, 180, 0));

        MainObjectManager.Instance.zombies.Add(zombie);

        zombie.GetComponent<NavAgentSimple>().SetDestination(target);
    }

    private Transform randomTargetFromTargets()
    {
        return MainObjectManager.Instance.bloky[UnityEngine.Random.Range(0, MainObjectManager.Instance.bloky.Length)].transform.GetChild(0);
    }
}
