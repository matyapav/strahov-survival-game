using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSpawner : MonoBehaviour {

    public GameObject ZombiePrefab;
    public int numberOfZombies = 10;
    public MainObjectManager mom;
    private Animator animator;
    private MessageController messageController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        messageController = GameObject.Find("SceneController").GetComponent<MessageController>();
    }

    public void Arrive()
    {
        animator.SetBool("active", true);
        messageController.AddMessage("Prepare yourself! Bus n. 149 is arriving!!!", 6f, Color.green);
    }

    public void Leave()
    {
        animator.SetBool("active", false);
    }

    public void SpawnWave () {
        for (int i = 0; i < numberOfZombies; i++) {
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        Transform target = randomTargetFromTargets();
        ZombiePrefab.GetComponent<NavAgentSimple>().SetDestination(target);
        Instantiate(ZombiePrefab, transform.position, Quaternion.identity);
    }

    private Transform randomTargetFromTargets()
    {
        return mom.bloky[UnityEngine.Random.Range(0, mom.bloky.Length)].transform;
    }
}
