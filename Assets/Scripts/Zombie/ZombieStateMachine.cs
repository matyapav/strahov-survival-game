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
    // All the possible states of the zombie
    public enum ZombieStateEnum {
        SeekPath,   // Seek path is called only when the zombie has no path
        Walking,    // When in walking mode
        Attacking,  // When in attacking mode
		Drinking,   // When in drinking mode (attacking a block)
        Dying       // If the zombie is dead but still in the scene
    };

    [HideInInspector]
    public UnityEvent OnSeekPath;
    [HideInInspector]
    public UnityEvent OnWalkingStart;
    [HideInInspector]
    public UnityEvent OnAttackStart;
	[HideInInspector]
	public UnityEvent OnDrinkingStart;
    [HideInInspector]
    public UnityEvent OnDyingStart;

    private ZombieStateEnum _state = ZombieStateEnum.Walking;
    public ZombieStateEnum State {
        get {
            return _state;
        }

        set {
            _state = value;

			if (value == ZombieStateEnum.SeekPath) {
				OnSeekPath.Invoke ();
			} else if (value == ZombieStateEnum.Walking) {
				OnWalkingStart.Invoke ();
			} else if (value == ZombieStateEnum.Attacking) {
				OnAttackStart.Invoke ();
			} else if (value == ZombieStateEnum.Drinking) {
				OnDrinkingStart.Invoke ();
			} else if (value == ZombieStateEnum.Dying) {
                OnDyingStart.Invoke();
            }
        }
    }

    public bool IsSeekPath() {
        return State == ZombieStateEnum.SeekPath;
    }

    public bool IsWalking() {
        return State == ZombieStateEnum.Walking;
    }

    public bool IsAttacking() {
        return State == ZombieStateEnum.Attacking;
    }

	public bool IsDrinking(){
		return State == ZombieStateEnum.Drinking;
	}

    public bool IsDying() {
        return State == ZombieStateEnum.Dying;
    }
}
