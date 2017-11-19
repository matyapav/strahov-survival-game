using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Zombie navigation controller.
/// </summary>
/// <remarks>
/// This script is used for targetting and moving the Zombie
/// </remarks>
[RequireComponent(typeof(ZombieHealth))]
[RequireComponent(typeof(ZombieStateMachine))]
[RequireComponent(typeof(CapsuleCollider))]
public class ZombieNavigationController : MonoBehaviour {

    public float movementSpeed = 2.7f;
    public float stoppingDistance = 1.0f;
    public float angleTurnLimit = 60;

    private ZombieStateMachine zombieStateMachine;
    private NavMeshAgent agent;

    public Transform target;

    // Add the events
    private void OnEnable()
    {
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        zombieStateMachine.OnSeekPath.AddListener(SeekANewtarget);
        zombieStateMachine.OnAttackStart.AddListener(StopNavigation);
        zombieStateMachine.OnDying.AddListener(StopNavigation);
    }

    // Set some hardcoded stuff to the Agent
    private void SetAgentSeeting() {
        agent.speed = movementSpeed;
        agent.angularSpeed = 900;   // Huge number helps with clumsy turning
        agent.acceleration = 5;

        // Removes ugly sliding on the last few steps
        // but causes the agent to overshoot the target position sometimes
        agent.autoBraking = false;
    }

    // Seek a new target and start navigation
    private void SeekANewtarget() 
    {
        // TODO: Add more things than only bloks
        Transform _target = MainObjectManager.Instance.GetRandomBlock().transform;
        SeekANewtarget(_target);
    }

    // Seek a new target and start navigation
    private void SeekANewtarget(Transform _target)
    {
        target = _target;
        agent.isStopped = false;
        agent.SetDestination(target.position);

        zombieStateMachine.SetCurrentState(ZombieStateMachine.ZombieStateEnum.Walking);
    }

    // Stop the agent from navigating and moving
    private void StopNavigation() {
        agent.isStopped = true;
    }

    private void Update()
    {
        if (zombieStateMachine.IsWalking()) {
            // Stop pathfinding on the last step
            if (Vector3.Magnitude(transform.position - agent.destination) <= stoppingDistance) {
                zombieStateMachine.OnAttackStart.Invoke();
            }

            // Limit movement speed when doing sharp turns
            float turnAngle = Vector3.Angle(transform.forward, agent.destination);
            if (turnAngle > angleTurnLimit) {
                agent.speed = 0.3f;
            }
            else {
                agent.speed = movementSpeed;
            }    
        }
        else if(zombieStateMachine.IsAttacking()) {
            // If the target is null, start seeking the path again
            if (target == null) {
               zombieStateMachine.SetCurrentState(ZombieStateMachine.ZombieStateEnum.SeekPath);
            } 
            else {
                // TODO: attack logic = decrease blocks health,..   
            }
        }
    }

    // The bloks must have a static rigid body
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Blok") {
            target = collision.gameObject.transform;
            zombieStateMachine.SetCurrentState(ZombieStateMachine.ZombieStateEnum.Attacking);
        }
    }
}
