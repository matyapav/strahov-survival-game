using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainEventManager : MonoBehaviourSingleton<MainEventManager> {

    // Only show those that should be visible
    public UnityEvent SpawnWaveEvent;

    [HideInInspector] 
    public UnityEvent OnDaySwitchPhase;
    [HideInInspector]
    public UnityEvent OnBusLeaving;
    [HideInInspector]
    public UnityEvent OnBlackMarketClicked;
    [HideInInspector]
    public UnityEvent<int> OnBlokClicked;
    [HideInInspector]
    public UnityEvent<GameObject> OnZombieSpawn;

    // All invokes that will be called from the GUI events must have an invoke function
    public void SwitchDayPhaseEventInvoke() {
        OnDaySwitchPhase.Invoke();
    }

    public void SpawnWaveEventInvoke() {
        SpawnWaveEvent.Invoke();
    }

    public void OnBusLeavingInvoke() {
        OnBusLeaving.Invoke();
    }
}
