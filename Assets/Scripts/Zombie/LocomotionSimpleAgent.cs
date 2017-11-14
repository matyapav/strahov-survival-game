using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionSimpleAgent : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;

	private float distRemaining = 0.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Don’t update position automatically
        agent.updatePosition = false;
		agent.acceleration = 6;
		agent.speed = 3;
		agent.angularSpeed = 900;
    }

    void Update()
    {
		// DEBUG BLOCK
		// Kills the zombie on K key press
		if (Input.GetKeyDown (KeyCode.K)) {
			anim.SetTrigger ("die");
			agent.isStopped = true;
		}
		// Navigate to mouse cursor position on LMB click
		/*if (Input.GetMouseButtonDown (1)) {
				RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				agent.destination = hit.point;
			}
		}*/
		// END DEBUG BLOCK

		/*
		Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

		// Map 'worldDeltaPosition' to local space
		float dx = Vector3.Dot(transform.right, worldDeltaPosition);
		float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
		Vector2 deltaPosition = new Vector2(dx, dy);
		
		// Low-pass filter the deltaMove
		float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
		smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);
		
		// Update velocity if time advances
		if (Time.deltaTime > 1e-5f)
			velocity = smoothDeltaPosition / Time.deltaTime;
		
		bool shouldMove = velocity.magnitude > 0.5f && (agent.remainingDistance > agent.radius);
		
		// Update animation parameters
		anim.SetBool("isWalking", shouldMove);
		*/

		// TODO
		// i když jsou nahardkóděný proměnný naprosto f pohodě, chtělo by to zpomalovat progresivně
		if (agent.remainingDistance < 2.5f) {
			agent.speed = 1;
		} else {
			agent.speed = 3;
		}

		anim.SetBool ("isWalking", agent.remainingDistance > 1.8f);

    }

	void OnAnimatorMove() 
	{
		// Move the transform
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
		transform.position = agent.nextPosition;
	}

 }