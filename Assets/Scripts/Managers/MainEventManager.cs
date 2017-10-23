using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainEventManager : MonoBehaviourSingletonPersistent<MainEventManager> {

    public UnityEvent SwitchDayPhaseEvent;
    public UnityEvent SpawnWaveEvent;

}
