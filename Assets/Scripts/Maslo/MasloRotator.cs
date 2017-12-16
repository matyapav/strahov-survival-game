using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasloRotator : MonoBehaviour {

    public float rotationSpeed = 1f;
	
	void Update () {
		transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
	}
}
