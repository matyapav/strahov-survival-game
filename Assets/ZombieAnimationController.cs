using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Zombie animation controller.
/// </summary>
/// <remarks>
/// This scripts takes care of switching the animations in the Zombie
/// </remarks>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ZombieHealth))]
[RequireComponent(typeof(ZombieStateMachine))]
public class ZombieAnimationController : MonoBehaviour {

    private ZombieStateMachine zombieStateMachine;
    private Animator anim;

    private void OnEnable()
    {
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        zombieStateMachine.OnDying.AddListener(PlayDieAnimation);
        zombieStateMachine.OnWalkingStart.AddListener(SetWalking);
        zombieStateMachine.OnAttackStart.AddListener(SetAttacking);

        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void PlayDieAnimation() {
        anim.SetTrigger("die");
    }

    private void SetWalking() {
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
    }

    private void SetAttacking() {
        anim.SetBool("isAttacking", true);
        anim.SetBool("isWalking", false);
    }
}
