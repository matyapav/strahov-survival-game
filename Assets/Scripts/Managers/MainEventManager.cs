using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainEventManager : MonoBehaviourSingleton<MainEventManager> {

    // Only show those that should be visible
    public UnityEvent SpawnWaveEvent;

    public UnityEvent OnDaySwitchPhase;
    [HideInInspector]
    public UnityEvent OnBusLeaving;
    [HideInInspector]
    public UnityEvent OnBusArrived;
    public UnityEvent OnBlackMarketClicked;
    [HideInInspector]
    public UnityEvent<int> OnBlokClicked;
    [HideInInspector]
    public GameObjectEvent OnZombieSpawn;

    // All invokes that will be called from the GUI events must have an invoke function
    public void SwitchDayPhaseEventInvoke() {
        OnDaySwitchPhase.Invoke();
    }

    public void SpawnWaveEventInvoke() {
        SpawnWaveEvent.Invoke();
    }

    public void OnBusArrivedInvoke()
    {
        OnBusArrived.Invoke();
    }

    public void OnBusLeavingInvoke() {
        OnBusLeaving.Invoke();
    }
}
