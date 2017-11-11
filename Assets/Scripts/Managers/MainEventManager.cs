using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainEventManager : MonoBehaviourSingletonPersistent<MainEventManager> {

    public UnityEvent SwitchDayPhaseEvent;
    public UnityEvent SpawnWaveEvent;

    public UnityEvent GamePauseEvent;
    public UnityEvent GameResumeEvent;

    public UnityEvent OnBusLeaving;
    public GameObjectEvent OnZombieSpawn;

    // Initialise the UnityEvents that requie it 
    private void OnEnable()
    {
        // OR NOT? not sure lol
        //if (OnZombieSpawn == null) {
        //    OnZombieSpawn = new GameObjectEvent();
        //}
    }

    // All invokes that will be called from the GUI events must have an invoke function
    public void SwitchDayPhaseEventInvoke() {
        SwitchDayPhaseEvent.Invoke();
    }

    public void SpawnWaveEventInvoke() {
        SpawnWaveEvent.Invoke();
    }

    public void PauseGameEventInvoke() {
        GamePauseEvent.Invoke();
    }

	public void ResumeGameEventInvoke() {
        GameResumeEvent.Invoke();
    }

    public void OnBusLeavingInvoke() {
        OnBusLeaving.Invoke();
    }
}
