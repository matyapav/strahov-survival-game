using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour {

    public int busNumber;
    public GameObject ZombiePrefab;
    public int numberOfZombies = 10;
    public MainObjectManager mom;
    private Animator animator;
    private MessageController messageController;
    private float timeBetweenSpawns = 0.75f;

    public delegate void OnBusLeaving();
    public OnBusLeaving onBusLeaving;

    private void Start()
    {
        animator = GetComponent<Animator>();
        messageController = GameObject.Find("SceneController").GetComponent<MessageController>();
    }

    public void Arrive()
    {
        animator.SetBool(animator.GetParameter(0).name, true);
        messageController.AddMessage("Prepare yourself! Bus n. "+ busNumber +" is arriving!!!", 6f, Color.green);
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
        GameObject zombie = (GameObject) Instantiate(ZombiePrefab, transform.position, Quaternion.identity);
        zombie.GetComponent<NavAgentSimple>().SetDestination(target);

    }

    private Transform randomTargetFromTargets()
    {
        //TODO remove get child 0 once pivots on buidings are in the right place
        return mom.bloky[UnityEngine.Random.Range(0, mom.bloky.Length)].transform.GetChild(0);
    }
}
