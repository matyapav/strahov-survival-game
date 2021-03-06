﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingController : MonoBehaviour {

	public GameObject[] guns;

	public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
	public float range = 30f;
	public GameObject target;
	public int bullets = 20;
	private int bulletsBackup;
	public Text bulletsCountUI;
	public float reloadTime = 1.5f;
	private float reloadTimer;
	private int gunIndex = 0;
    private float timer;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
	private int shootableLayer;
	public GameObject reloadUI;
	public Image reloadProgressImage;
	private bool reloading = false;
    public Light flashlight;

    private void OnEnable()
    {
        // Deactivate reload UI
        MainEventManager.Instance.OnDaySwitchPhase.AddListener(DeactivateReloadUI);
    }

    // Use this for initialization
    void Awake() {
		shootableLayer = LayerMask.GetMask("Shootable");
		Vector3 targetPos = target.transform.position;
		targetPos.z = range - 5f;
		target.transform.position = targetPos;
		bulletsBackup = bullets;
		bulletsCountUI.text = bullets + "/" +bulletsBackup;
	} 

	void Update() {
        if (DayNightController.Instance.Phase == DayNightPhase.NIGHT) {
            HandleShooting();
			
			if(reloading) {
				Reload();
			}

			// Update bullets count ui
			bulletsCountUI.text = bullets+"/"+bulletsBackup;
        }
        else {
            Debug.LogError("Player shooting controller is active during the day!");
        }
	}

	void HandleShooting() {
        timer += Time.deltaTime;
		Debug.DrawRay(transform.position, range * transform.forward, Color.red);
		if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.R)){
			reloading = true;
		}
        if (Input.GetKeyDown(KeyCode.F)) {
            flashlight.enabled = !flashlight.enabled;
        }
		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && bullets > 0 && !reloading) {
			Shoot ();
		}
	}

	private void Reload() {
		if (!reloadUI.activeInHierarchy){
			reloadUI.SetActive(true);
		}
		reloadTimer += Time.deltaTime;
		reloadProgressImage.fillAmount = reloadTimer/reloadTime;
		if (reloadTimer >= reloadTime){
			DeactivateReloadUI();
		}
	}

    private void DeactivateReloadUI() {
        reloadProgressImage.fillAmount = 0f;
        reloadTimer = 0f;
        reloadUI.SetActive(false);
        bullets = bulletsBackup;
        reloading = false;
    }

	private void Shoot() 
    {
        timer = 0f;
		GameObject gun =  guns[gunIndex % guns.Length];
		ParticleSystem ps = gun.GetComponentInChildren<ParticleSystem>();
		gun.GetComponent<AudioSource>().Play();
        shootRay.origin = gun.transform.position;
        shootRay.direction = transform.forward;

        ps.Stop();
        ps.Play();

        if (Physics.SphereCast (shootRay, 2f, out shootHit, range, shootableLayer)) {
			if (shootHit.transform.gameObject.tag == "Zombie") {
				shootHit.transform.GetComponent<ZombieHealth>().Damage(damagePerShot);
			}
        }

		gunIndex++;
		bullets--;
	}
}
