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
    public static bool DEBUG = false;

    public float movementSpeed = 2.7f;
    public float stoppingDistance = 1.0f;
    public float angleTurnLimit = 60;
    public float damage = 5f;
    public float attackRange = 5f;

    private ZombieStateMachine zombieStateMachine;
    private NavMeshAgent agent;

    public Transform navigation_target = null;
    public GameObject attack_target = null;

    private Rigidbody rb;
    private float nextAttackTime = 0.0f;
    private float nextAttackIn = 1.5f;

    private void OnEnable()
    {
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody>();
		
        rb.isKinematic = false;

        // Add the events
        zombieStateMachine.OnSeekPath.AddListener(SeekNavigationTargetAfterAttacking);
        zombieStateMachine.OnWalkingStart.AddListener(ResumeNavigation);
        zombieStateMachine.OnAttackStart.AddListener(StopNavigation);
        zombieStateMachine.OnDrinkingStart.AddListener(StopNavigation);
        zombieStateMachine.OnDyingStart.AddListener(StopNavigation);
    }

    // Stop the agent from navigating and moving
    private void StopNavigation() {
        agent.isStopped = true;
        rb.isKinematic = true;
    }

    private void ResumeNavigation() {
        agent.isStopped = false;
        rb.isKinematic = false;
    }

    // Seek for a new target
    void SeekNavigationTargetAfterAttacking()
    {
        // While debugging do not change the target 
        Transform newNavTarget = MainObjectManager.Instance.GetRandomActiveBlock().GetComponent<Blok>().GetRandomTarget();

        if (newNavTarget == null) {
            Debug.LogError("There is not target to be navigated to in the scene");
            return;
        }

        attack_target = null;
        navigation_target = newNavTarget;

        agent.isStopped = false;
        rb.isKinematic = false;
        agent.SetDestination(navigation_target.position);

        zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.Walking;
    }

    private void Update()
    {
        if (zombieStateMachine.IsSeekPath())
        {
            // Should not happen
        }
        else if (zombieStateMachine.IsWalking())
        {
            // Do the attacking
            Ray front = new Ray(transform.position, transform.forward);
            Ray left = new Ray(transform.position, transform.forward - transform.right);
            Ray right = new Ray(transform.position, transform.forward + transform.right);

            AttackRay(front);
            AttackRay(left);
            AttackRay(right);
        }
        else if (zombieStateMachine.IsDrinking() || zombieStateMachine.IsAttacking())
        {
            // Do checking
            if (attack_target == null)
            {
                zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.SeekPath;
                return;
            }

            IDamageable<float> damagable_attack_target = GetDamageableFromGO(attack_target);

            if (damagable_attack_target == null)
            {
                zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.SeekPath;
                return;
            }

            // Rotate towards target
            Vector3 lookTarget = attack_target.transform.position;
            lookTarget.y = transform.position.y;

            transform.LookAt(lookTarget);

            // Attack once upon time
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + nextAttackIn;

                damagable_attack_target.Damage(damage);

                if (damagable_attack_target.Dead())
                {
                    Debug.Log("it is dead");
                    attack_target = null;
                    navigation_target = null;

                    zombieStateMachine.State = ZombieStateMachine.ZombieStateEnum.SeekPath;
                    return;
                }
            }
        }
        else if(zombieStateMachine.IsDying()) {
            
        }
    }

    // Draw the attacking gizmo in front of the zombie
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Ray front = new Ray(transform.position, transform.forward);
            Ray left = new Ray(transform.position, transform.forward - transform.right);
            Ray right = new Ray(transform.position, transform.forward + transform.right);

            // Visual debugging of the zombies attack raycasts
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(front.origin, front.origin + front.direction * attackRange);
            Gizmos.DrawLine(left.origin, left.origin + left.direction * attackRange);
            Gizmos.DrawLine(right.origin, right.origin + right.direction * attackRange);

            // Visual debugging of FSM
            if(zombieStateMachine.IsAttacking()) Gizmos.color = Color.red;
            else if (zombieStateMachine.IsWalking()) Gizmos.color = Color.green;
            else if (zombieStateMachine.IsDrinking()) Gizmos.color = Color.yellow;
            else if (zombieStateMachine.IsSeekPath()) Gizmos.color = Color.magenta;
            else Gizmos.color = Color.grey;

            // Draw a cube at zombies feet
            Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.one * 3f);
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
                attack_target = ghit;

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
  

// Limit movement speed when doing sharp turns
//float turnAngle = Vector3.Angle(transform.forward, agent.destination);
//if (turnAngle > angleTurnLimit) {
//    agent.speed = 0.3f;
//}
//else {
//    agent.speed = movementSpeed;
//}
