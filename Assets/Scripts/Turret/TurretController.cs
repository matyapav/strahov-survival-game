using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NeighbourObjectTracker))]
public class TurretController : MonoBehaviour, IDamageable<float> {

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

    // TODO: do this better
    public float HP = 100f;

    public GameObject rangeCylinder;

    private Transform TurretTop;
    private ParticleSystem turretParticleSystem;
    private float lastTimeShot;         // used for the time delay
    private AudioSource audioSource;    // used to play tunes
    private NeighbourObjectTracker neighbourObjectTracker;
    private bool active = false;

	void Start () {
        // The nozzle flash
        turretParticleSystem = GetComponentInChildren<ParticleSystem>();

        // Get the rotating barrel form the children
        TurretTop = transform.GetChild(0);

        lastTimeShot = Time.time;
        audioSource = GetComponent<AudioSource>();

        // Init the neighbour tracker
        neighbourObjectTracker = GetComponent<NeighbourObjectTracker>();
        neighbourObjectTracker.Init(range);

        rangeCylinder.transform.localScale = new Vector3(range / transform.localScale.x, 0.0001f, range / transform.localScale.x);
	}
	
	void Update () {
        // TODO: do this better
        if(active){
            foreach (GameObject g in neighbourObjectTracker.trackedObjects) {
                if(g != null) {
                    ZombieStateMachine z = g.GetComponent<ZombieStateMachine>();

                    if(!z.IsDying()) {
                        SeekTarget(g.transform);
                        break;
                    }
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

            // Get the damageable interface
            IDamageable<float> damageable = _target.GetComponent<ZombieHealth>() as IDamageable<float>;

            // Damage the zombie
            if (!damageable.Dead()) {
                damageable.Damage(damage);    
            }
        }
    }

    public void Activate(bool a) {
        active = a;
    }

    public bool Dead() {
        return HP <= 0;
    }

    public void Damage(float dmg) {
        HP -= dmg;
    }
}
