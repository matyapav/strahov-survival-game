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

    private ZombieStateEnum _state;
    public ZombieStateEnum State {
        get {
            return _state;
        }
        set {
            if (value == ZombieStateEnum.SeekPath)
            {
                OnSeekPath.Invoke();
            }
            else if (value == ZombieStateEnum.Walking)
            {
                OnWalkingStart.Invoke();
            }
            else if (value == ZombieStateEnum.Attacking)
            {
                OnAttackStart.Invoke();
            }
            else if (value == ZombieStateEnum.Dying)
            {
                OnDying.Invoke();
            }

            _state = value;
        }
    }

    // On spawn start seeking a target
    private void Start()
    {
        State = ZombieStateEnum.SeekPath;
    }

    // Some bool checks
    public bool IsWalking() {
        return State == ZombieStateEnum.Walking;
    }

    public bool IsAttacking() {
        return State == ZombieStateEnum.Attacking;
    }

    public bool IsDying() {
        return State == ZombieStateEnum.Dying;
    }
}
