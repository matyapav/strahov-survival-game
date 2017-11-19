using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NeighbourObjectTracker))]
public class TurretController : MonoBehaviour {

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

    private Transform TurretTop;
    private ParticleSystem turretParticleSystem;
    private float lastTimeShot;         // used for the time delay
    private AudioSource audioSource;    // used to play tunes
    private NeighbourObjectTracker neighbourObjectTracker;

	void Start () {
        turretParticleSystem = GetComponentInChildren<ParticleSystem>();
        TurretTop = transform.GetChild(0);
        lastTimeShot = Time.time;
        audioSource = GetComponent<AudioSource>();

        // Init the neighbour tracker
        neighbourObjectTracker = GetComponent<NeighbourObjectTracker>();
        neighbourObjectTracker.Init(range);
	}
	
	void Update () {
        // TODO: do this better
        foreach (GameObject g in neighbourObjectTracker.trackedObjects) {
            if(g != null) {
                ZombieStateMachine z = g.GetComponent<ZombieStateMachine>();

                if(!z.IsDying()) {
                    SeekTarget(g.transform);
                }
            }
        }
	}

    void SeekTarget(Transform _target) {
        if (transform.gameObject.activeSelf) {
            if(IsInRange(transform, _target, range)) {
				// Rotate towards the target and shoot
				if (_target != null && TurretTop != null)
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
    }

    public static bool IsInRange(Transform _position, Transform _target, float _range) {
        return ((_target.position - _position.position).magnitude < _range);
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
