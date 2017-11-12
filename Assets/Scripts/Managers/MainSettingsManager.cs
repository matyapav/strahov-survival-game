using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainSettingsManager : MonoBehaviourSingletonPersistent<MainSettingsManager> {
    
    public enum DayPhase {Day, Night}

    private DayPhase _currentDayPhase = DayPhase.Day;
    public DayPhase currentDayPhase {
        get {
            return _currentDayPhase;
        }
    }

    private int _level = 0;
    public int level {
        get {
            return _level;
        }  
    }

    private int _money = 0;
    public int money {
        get {
            return _money;
        }
    }

    // Continues to the next phase and returns it
    private void SwitchPhase() {
        if(currentDayPhase == DayPhase.Day) {
            _currentDayPhase = DayPhase.Night;
        }
        else {
            _currentDayPhase = DayPhase.Day;
        }
    }
}
