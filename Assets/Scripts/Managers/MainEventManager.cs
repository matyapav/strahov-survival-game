using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainEventManager : MonoBehaviourSingleton<MainEventManager> {

    public UnityEvent SwitchDayPhaseEvent;
    public UnityEvent SpawnWaveEvent;

    public UnityEvent GamePauseEvent;
    public UnityEvent GameResumeEvent;

    public void SwitchDayPhaseEventInvoke() {
        SwitchDayPhaseEvent.Invoke();
    }

    public void SpawnWaveEventInvoke() {
        SpawnWaveEvent.Invoke();
    }

    public void PauseGameEventInvoke() {
        GamePauseEvent.Invoke();
    }

    public void ResumeGameEventInvoke()
    {
        GameResumeEvent.Invoke();
    }
}
