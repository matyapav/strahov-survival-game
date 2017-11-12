using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(AudioSource))]
public class TurretController : MonoBehaviour {

    private Transform TurretTop;
    private ParticleSystem turretParticleSystem;

    [Tooltip("The target transform")]
    public Transform TargetTransform;

    [Tooltip("The speed of rotation of the turret")]
    public float rotationSpeed = 3f;

    [Tooltip("The delay between shots of the turret in seconds")]
    public float fireDelay = 1f;

    [Tooltip("Damage of the turret")]
    public float damage = 10f;

    [Tooltip("The range of the turret")]
    public float range = 100f;

    [Tooltip("The widhth of the field of attack")]
    public float spread = 10f;


    private float lastTimeShot;         // used for the time delay
    private AudioSource audioSource;    // used to play tunes

	void Start () {
        turretParticleSystem = GetComponentInChildren<ParticleSystem>();
        TurretTop = transform.GetChild(0);
        lastTimeShot = Time.time;
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
        // TODO use the object manager or some effective way
        GameObject[] allGo = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject g in allGo) {
            if (g.activeSelf){
                SeekTarget(g.transform);
            }
        }
	}

    void SeekTarget(Transform _target) {
        // Rotate towards the target and shoot
        if (_target != null && TurretTop != null)
        {
            if ((_target.position - transform.position).magnitude < range)
            {
                float currentDeltaAngle = RotateTowardsTarget(_target);
                // If the target is small enyought, shoot
                if (Mathf.Abs(currentDeltaAngle - 360) % 360 < spread || Mathf.Abs(currentDeltaAngle) % 360 < spread)
                {
                    Shoot(_target);
                }
            }
        }
    }

    float RotateTowardsTarget(Transform _target) {
        // Delta vector to the target
        Vector3 delta = _target.position - TurretTop.transform.position;

        // Get the current local rotation of the top
        Vector3 curRotation = TurretTop.transform.localRotation.eulerAngles;

        // Get the current rotation and calculate the desired
        float desiredZRotation = Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg - 90;

        // Lerp and assign
        TurretTop.transform.localRotation =
            Quaternion.Euler(0, 0, Mathf.LerpAngle(curRotation.z, desiredZRotation, Time.deltaTime * rotationSpeed));

        return desiredZRotation - curRotation.z;
    }


    void Shoot(Transform _target) {
        if (Time.time - lastTimeShot > fireDelay) {
            // Update the delay
            lastTimeShot = Time.time;

            // Play the particle shoot animation
            turretParticleSystem.Play();

            // Play the sound
            audioSource.Play();

            // Damage the zombie
            ZombieHealth zombieHealth = _target.GetComponent<ZombieHealth>();
            if (zombieHealth != null) {
                zombieHealth.GetHit(damage);    
            }
        }
    }
}
