using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TurretController : MonoBehaviour {

    private Transform TurretTop;
    public Transform TargetTransform;
    private ParticleSystem turretParticleSystem;

    public float rotationSpeed = 3f;

	void Start () {
        turretParticleSystem = GetComponentInChildren<ParticleSystem>();
        TurretTop = transform.GetChild(0);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            turretParticleSystem.Play();    
        }

        if (TargetTransform != null && TurretTop != null) {
            // Delta vector to the target
             Vector3 delta = TargetTransform.position - TurretTop.transform.position;

            // Get the current local rotation of the top
            Vector3 curRotation = TurretTop.transform.localRotation.eulerAngles;

            // Get the current rotation and calculate the desired
            float desiredZRotation = Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg - 90;

            // Lerp and assign
            TurretTop.transform.localRotation = 
                Quaternion.Euler(0, 0, Mathf.LerpAngle(curRotation.z, desiredZRotation, Time.deltaTime * rotationSpeed));
        }
	}
}
