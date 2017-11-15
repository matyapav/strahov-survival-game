using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionSimpleAgent : MonoBehaviour
{
	public float movementSpeed = 2.7f;
	public float lastStepLength = 0.4f;
	public float stoppingDistance = 1.0f;
	public float angleTurnLimit = 60;

    private Animator anim;
    private NavMeshAgent agent;

	private Vector3 destination;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

		agent.speed = movementSpeed;
		agent.angularSpeed = 900; //huge number helps with clumsy turning
		agent.acceleration = 5;

		// Removes ugly sliding on the last few steps
		// but causes the agent to overshoot the target position sometimes
		agent.autoBraking = false;
    }

    void Update()
    {
		// DEBUG BLOCK
		// Kills the zombie on pressed K key
		if (Input.GetKeyDown (KeyCode.K)) {
			anim.SetTrigger ("die");
			agent.isStopped = true;
		}
		// Navigate to mouse cursor position on LMB click
		// Double click needed for some reason
		/*
		if (Input.GetMouseButtonDown (1)) {
				RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				agent.isStopped = false;
				destination = hit.point;
			}
		}
		*/
		// END DEBUG BLOCK
    }

	void FixedUpdate()
	{
		// Stop pathfinding on the last step
		if (agent.remainingDistance < lastStepLength) {
			agent.isStopped = true;
		} 

		// Limit movement speed when doing sharp turns
		float turnAngle = Vector3.Angle (transform.forward, agent.destination);
		if (turnAngle > angleTurnLimit) {
			agent.speed = 0.3f;
		} else {
			agent.speed = movementSpeed;
		}

		// Call the correct animation
		// TODO
		anim.SetBool ("isWalking", agent.remainingDistance > stoppingDistance && turnAngle<=angleTurnLimit);
		anim.SetBool ("isAttacking", agent.remainingDistance <= stoppingDistance); //TODO && nearObstacle && pathIncomplete
	}

	// Set a new destination point
	void LateUpdate(){
		if (!destination.Equals (agent.destination)) {
			agent.SetDestination (destination);
		}
	}
 }