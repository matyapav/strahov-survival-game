﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusSpawner : MonoBehaviour {

    public string busName;
    public GameObject[] ZombiePrefabs;
    private Animator animator;
    public float timeBetweenSpawns = 0.75f;

    private AudioSource spawnSound;

    public AudioClip spawn1;
    public AudioClip spawn2;
    public AudioClip spawn3;
    public AudioClip spawn4;
    public AudioClip spawn5;


    private void Start()
    {
        animator = GetComponent<Animator>();
        spawnSound = GetComponent<AudioSource>();
    }

    public void Arrive()
    {
        MainUISoundManager.Instance.PlaySound("bus_ride");
        animator.SetBool(animator.GetParameter(0).name, true);
        MainEventManager.Instance.OnBusDispatched.Invoke();
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
        MainEventManager.Instance.OnBusArrived.Invoke();

        // Invoke the Spawning of the zombie
        // TODO: Add wave id info 
        int number_of_zombies = DayNightController.Instance.NumberOfZombiesInWave();
        Debug.Log("Spawning " + number_of_zombies + " zombies.");
        StartCoroutine(SpawnZombies(number_of_zombies, timeBetweenSpawns, -1));
    }

    // Spawn a zombie
    IEnumerator SpawnZombies(int number, float waittime, int wave_id)
    {
        for (int i = 0; i < number; i++) {
            // get random sound and play it
            selectSpawnSound();
            spawnSound.Play();

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

            yield return new WaitForSeconds(waittime);
        }

        MainUISoundManager.Instance.PlaySound("bus_ride");
        // Start the animation
        animator.SetBool(animator.GetParameter(0).name, false);

        // Invoke the event in the EventManager
        MainEventManager.Instance.OnBusLeaving.Invoke();
    }

    private void selectSpawnSound()
    {
        int randomInt = UnityEngine.Random.Range(0, 4);

        switch (randomInt)
        {
            case 0:
                spawnSound.clip = spawn1;
                break;
            case 1:
                spawnSound.clip = spawn2;
                break;
            case 2:
                spawnSound.clip = spawn3;
                break;
            case 3:
                spawnSound.clip = spawn4;
                break;
            case 4:
                spawnSound.clip = spawn5;
                break;
        }
    }
}
