using System;
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
public class ZombieNavigationController : MonoBehaviour
{

    public float movementSpeed = 2.7f;
    public float stoppingDistance = 1.0f;
    public float angleTurnLimit = 60;
    public float damage = 5f;
    public float attackRange = 5f;

    private ZombieStateMachine zombieStateMachine;
    private NavMeshAgent agent;

    public Transform navigation_target;
    public IDamageable<float> attack_target = null;

    private Rigidbody rb;
    private float nextAttackTime = 0.0f;
    private float nextAttackIn = 1.5f;

    // Add the events
    private void OnEnable()
    {
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        agent = GetComponent<NavMeshAgent>();

        zombieStateMachine.OnSeekPath.AddListener(SeekNavigationTargetAfterAttacking);
        zombieStateMachine.OnAttackStart.AddListener(StopNavigation);
        zombieStateMachine.OnDying.AddListener(StopNavigation);

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    // Stop the agent from navigating and moving
    private void StopNavigation()
    {
        agent.isStopped = true;
    }

    // Seek for a new target
    void SeekNavigationTargetAfterAttacking()
    {
        attack_target = null;
        Transform newNavTarget = MainObjectManager.Instance.GetRandomBlock().GetComponent<Blok>().GetRandomTarget();

        if (newNavTarget == null) {
            Debug.LogError("There is not target to be navigated to in the scene");
            return;
        }

        SetupNavigationTarget(newNavTarget);
    }

    // Seek a new target and start navigation
    private void SetupNavigationTarget(Transform _target)
    {
        navigation_target = _target;
        agent.isStopped = false;
        rb.isKinematic = false;
        agent.SetDestination(navigation_target.position);
        zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.Walking;
    }

    private void Update()
    {
        // If it is not attacking or dying, check if there is anything around to hit
        if (!zombieStateMachine.IsAttacking() && !zombieStateMachine.IsDying() || zombieStateMachine.IsWalking()) // bit of nonsense overkill
        {
            Ray front = new Ray(transform.position, transform.forward);
            Ray left = new Ray(transform.position, transform.forward - transform.right);
            Ray right = new Ray(transform.position, transform.forward + transform.right);

            AttackRay(front);
            AttackRay(left);
            AttackRay(right);
        }
        else if (zombieStateMachine.IsWalking())
        {
            // Limit movement speed when doing sharp turns
            //float turnAngle = Vector3.Angle(transform.forward, agent.destination);
            //if (turnAngle > angleTurnLimit) {
            //    agent.speed = 0.3f;
            //}
            //else {
            //    agent.speed = movementSpeed;
            //}
        }
        else if (zombieStateMachine.IsAttacking())
        {
            if (attack_target == null) {
                zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.SeekPath;
                return;
            }

            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + nextAttackIn;

				attack_target.Damage(damage);
				
				if (attack_target.Dead()) {
                    Debug.Log("it is dead");
                    attack_target = null;
                    navigation_target = null;

                    zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.SeekPath;
                    return;
				}
            }
        }
    }

    // Draw the attacking gizmo in front of the zombie
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;

            Ray front = new Ray(transform.position, transform.forward);
            Ray left = new Ray(transform.position, transform.forward - transform.right);
            Ray right = new Ray(transform.position, transform.forward + transform.right);


            Gizmos.DrawLine(front.origin, front.origin + front.direction * attackRange);
            Gizmos.DrawLine(left.origin, left.origin + left.direction * attackRange);
            Gizmos.DrawLine(right.origin, right.origin + right.direction * attackRange);
        }
    }

    // Attack along the ray
    private void AttackRay(Ray r) {
        RaycastHit hit;
        if (Physics.Raycast(r.origin, r.direction, out hit, attackRange))
        {
            GameObject ghit = hit.transform.gameObject;
            if (IsAttackableTag(ghit)) 
            {

                IDamageable<float> id = GetDamageableFromGO(ghit);

                if (id == null) {
                    Debug.LogError("Something went horribly wrong. Zombie wants to attack an target that does not implement IDamageable.");
                }

                navigation_target = null;
                attack_target = id;

                zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.Attacking;
            }
        }
    }

    // Check if the tags are allright to be attacked
    private static bool IsAttackableTag(GameObject go) {
        if (go.tag == "Blok" || go.tag == "Turret" || go.tag == "Prakazka") {
            return true;
        }
        return false;
    }

    private static bool IsDrinkAttackableTag(GameObject go) {
        if (go.tag == "Blok") {
            return true;
        }
        return false;
    }


    // Get an Interface from GameObject
    public static IDamageable<float> GetDamageableFromGO(GameObject go) {
        Component[] d_components = go.GetComponents(typeof(IDamageable<float>));

        if (d_components.Length < 1) 
            return null;
        
        return (IDamageable<float>)d_components[0];
    }
}
  
