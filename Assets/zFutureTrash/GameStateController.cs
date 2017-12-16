using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviourSingleton<GameStateController> {

    public enum DayPhase { 
        Day, Night 
    }

    // Start at day in the beginning
    private DayPhase _currentDayPhase = DayPhase.Day;
    private int _nights_survived = 0;

    public DayPhase currentDayPhase {
        get {
            return _currentDayPhase;
        }
    }

    public int nights_survived {
        get {
            return _nights_survived;
        }
    }

    // Continues to the next phase and returns it
    private void SwitchPhase() {
        if (currentDayPhase == DayPhase.Day) {
            _currentDayPhase = DayPhase.Night;
            _nights_survived++;
        }
        else {
            _currentDayPhase = DayPhase.Day;
        }
        MainEventManager.Instance.OnDaySwitchPhase.Invoke();
    }
}
