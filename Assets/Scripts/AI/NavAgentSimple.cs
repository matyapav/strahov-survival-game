﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentSimple : MonoBehaviour {

    private NavMeshAgent agent;

	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast with the previously constructed ray
        if (Physics.Raycast(ray, out hit, 100)) {
            // Set the target of the agent with a right click
			if (Input.GetKeyDown(KeyCode.Mouse1)) {
                NavMeshPath navMeshPath = new NavMeshPath();    // Create a new navmesh path
                agent.CalculatePath(hit.point, navMeshPath);    // Calculate the path

                // Check if the path exist
                if (navMeshPath.status == NavMeshPathStatus.PathComplete) {
                    Debug.Log("Path complete");
                } else if (navMeshPath.status == NavMeshPathStatus.PathPartial) {
                    Debug.Log("Path partial");
                } else if (navMeshPath.status == NavMeshPathStatus.PathInvalid) {
                    Debug.Log("Path invalid");
                }

                // Rotate and the assign
				StartCoroutine(RotateToTarget(navMeshPath, hit.point));
			}
        }
	}

	IEnumerator RotateToTarget(NavMeshPath _navMeshPath, Vector3 _agent_target) {
		agent.path.ClearCorners ();
		while (AngleToPath(_agent_target) > 5f) {
			Vector3 targetDir = _agent_target - transform.position;
			float step = 5f * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			Debug.DrawRay (transform.position, newDir, Color.red);
			transform.rotation = Quaternion.LookRotation (newDir);

			yield return new WaitForEndOfFrame ();
		}

		agent.path = _navMeshPath;
	}

	float AngleToPath(Vector3 _target) {
		// Get a copy of your forward vector
		Vector3 forward = transform.forward;
		Vector3 delta = _target - transform.position;

		forward.y = 0;
		delta.y = 0;

		return Vector3.Angle(transform.forward, delta);
	}
}
