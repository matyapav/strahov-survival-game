using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Zombie state machine.
/// </summary>
/// <remarks>
/// Tracks the current state of the zombie and invokes events when the state is changed
/// </remarks>
public class ZombieStateMachine : MonoBehaviour
{
    public enum ZombieStateEnum {SeekPath, Walking, Attacking, Dying};

    [HideInInspector]
    public UnityEvent OnSeekPath;
    [HideInInspector]
    public UnityEvent OnWalkingStart;
    [HideInInspector]
    public UnityEvent OnAttackStart;
    [HideInInspector]
    public UnityEvent OnDying;

    // Apparently Unity crashes when using get/set on enum
    private ZombieStateEnum currentState;

    // Set a state and invoke events
    public void SetCurrentState(ZombieStateEnum state) {
        if (state == ZombieStateEnum.SeekPath)
        {
            OnSeekPath.Invoke();
        }
        else if (state == ZombieStateEnum.Walking)
        {
            OnWalkingStart.Invoke();
        }
        else if (state == ZombieStateEnum.Attacking)
        {
            OnAttackStart.Invoke();
        }
        else if (state == ZombieStateEnum.Dying)
        {
            OnDying.Invoke();
        }

        currentState = state;
    }

    // Get the current state of the event machine
    public ZombieStateEnum GetCurrentState() {
        return currentState;
    }

    // On spawn start seeking a target
    private void Start()
    {
        SetCurrentState(ZombieStateEnum.SeekPath);
    }

    // Some bool checks
    public bool IsWalking() {
        return currentState == ZombieStateEnum.Walking;
    }

    public bool IsAttacking() {
        return currentState == ZombieStateEnum.Attacking;
    }

    public bool IsDying() {
        return currentState == ZombieStateEnum.Dying;
    }
}
