using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainSettings : MonoBehaviourSingletonPersistent<MainSettings> {
    
    #region Events
    public UnityEvent SwitchDayPhaseEvent;

    private void OnEnable() {
        SwitchDayPhaseEvent.AddListener(SwitchPhase);
    }

    private void OnDisable() {
        SwitchDayPhaseEvent.RemoveListener(SwitchPhase);
    }
    #endregion

    // The current phas of the game
    public enum DayPhase {
        Day,
        Night
    }

    // Start at day to prepare yurself
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

    // Event listener for void event
    private void SwitchPhase() {
        NextPhase();
    }

    // Continues to the next phase and returns it
    private DayPhase NextPhase() {
        if(currentDayPhase == DayPhase.Day) {
            _currentDayPhase = DayPhase.Night;
        }
        else {
            _currentDayPhase = DayPhase.Day;
        }

        return _currentDayPhase;
    }

}
