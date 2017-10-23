using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainSettings : MonoBehaviour {

    #region Singleton Instance definition
    private static bool applicationIsQuitting = false;
    private static object _lock = new object();
    private static MainSettings _instance;
    public static MainSettings Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("Main Settings Singleton");
                    go.AddComponent<MainSettings>();
                    DontDestroyOnLoad(go);
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
    #endregion

    #region Events
    public UnityEvent SwitchDayPhaseEvent;
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

    // Continues to the next phase and returns it
    public DayPhase NextPhase() {
        if(currentDayPhase == DayPhase.Day) {
            _currentDayPhase = DayPhase.Night;
        }
        else {
            _currentDayPhase = DayPhase.Day;
        }

        return _currentDayPhase;
    }



}
