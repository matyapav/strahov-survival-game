using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Transform))]
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
    public float range = 20f;

    [Tooltip("The widhth of the field of attack")]
    public float spread = 20f;

    private float lastTimeShot;

	void Start () {
        turretParticleSystem = GetComponentInChildren<ParticleSystem>();
        TurretTop = transform.GetChild(0);
        lastTimeShot = Time.time;
	}
	
	void Update () {
        // Rotate towards the target and shoot
        if (TargetTransform != null && TurretTop != null) {
            if ((TargetTransform.position - transform.position).magnitude < range) {
                float currentDeltaAngle = RotateTowardsTarget();
                // If the target is small enyought, shoot
                if (Mathf.Abs(currentDeltaAngle - 360)  % 360 < 10f || Mathf.Abs(currentDeltaAngle) % 360 < 10f) {
                    Shoot();
                }
            }    
        }
	}

    float RotateTowardsTarget() {
        // Delta vector to the target
        Vector3 delta = TargetTransform.position - TurretTop.transform.position;

        // Get the current local rotation of the top
        Vector3 curRotation = TurretTop.transform.localRotation.eulerAngles;

        // Get the current rotation and calculate the desired
        float desiredZRotation = Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg - 90;

        // Lerp and assign
        TurretTop.transform.localRotation =
            Quaternion.Euler(0, 0, Mathf.LerpAngle(curRotation.z, desiredZRotation, Time.deltaTime * rotationSpeed));

        return desiredZRotation - curRotation.z;
    }


    void Shoot() {
        if(Time.time - lastTimeShot > fireDelay) {
            lastTimeShot = Time.time;
            turretParticleSystem.Play();   
        }
    }
}
