﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightController : MonoBehaviourSingleton<DayNightController> {

    public Text dayNightText;

    public CameraDayController cameraMovementDay;
    public CameraNightController cameraMovementNight;

    public GameObject[] onlyDayObjects;
    public GameObject[] onlyNightObjects;

    private DayNightPhase phase = DayNightPhase.DAY;
    private int dayCounter = 0;

    private void OnEnable()
    {
        // CRASHES unity for some reason
        //MainEventManager.Instance.OnDaySwitchPhase.AddListener(_SwitchPhase);
    }

    // Get the current phase
    public DayNightPhase Phase 
    {
        get {
            return phase;
        }
    }

    // Which phase should be starting? => Day
    private void Start()
    {
        StartDayPhase();
    }

    public void SwitchPhase() {
        //MainEventManager.Instance.OnDaySwitchPhase.Invoke();
        _SwitchPhase();
    }

    private void _SwitchPhase()
    {       
        if (phase == DayNightPhase.DAY)
        {
            ScreenFader.Instance.onFadeOut.RemoveListener(StartDayPhase);
            ScreenFader.Instance.onFadeOut.AddListener(StartNightPhase);
        }
        else if (phase == DayNightPhase.NIGHT)
        {
            LightsController.Instance.SetLampsOn(false);
            ScreenFader.Instance.onFadeOut.RemoveListener(StartNightPhase);
            ScreenFader.Instance.onFadeOut.AddListener(StartDayPhase);
        }

        ScreenFader.Instance.FadeOut();

        Invoke("InvokeFadeIn", 2f); //to hide camera change
    }

    private void InvokeFadeIn() {
        ScreenFader.Instance.FadeIn();
    }

    private void StartNightPhase()
    {
        LightsController.Instance.SetLampsOn(true);
        LightsController.Instance.SetDirectionalLightOn(false);

        phase = DayNightPhase.NIGHT;

        MainUISoundManager.Instance.StopSound("day");
        MainUISoundManager.Instance.PlaySound("main");
        MainUISoundManager.Instance.PlaySound("night");

        dayNightText.text = "Night " + dayCounter;

        MainCanvasManager.Instance.HideBuildMenu();
        MainCanvasManager.Instance.HideEndDayButton();
        ActivateProperObjectsAndScripts();

        // Start spawning waves of zombies
        WavesController.Instance.SpawnNWaves(NumberOfWavesInCurrentDay(), 0, 20);
    }

    public void StartDayPhase()
    {
        LightsController.Instance.SetDirectionalLightOn(true);

        dayCounter++;

        phase = DayNightPhase.DAY;

        MainUISoundManager.Instance.StopSound("main");
        MainUISoundManager.Instance.StopSound("night");
        MainUISoundManager.Instance.PlaySound("day");

        dayNightText.text = "Day " + dayCounter;

        MainCanvasManager.Instance.ShowBuildMenu();
        MainCanvasManager.Instance.ShowEndDayButton();
        ActivateProperObjectsAndScripts();
    }

    private void ActivateProperObjectsAndScripts()
    {
        cameraMovementDay.enabled = IsDay;
        cameraMovementNight.enabled = IsNight;

        MainObjectManager.Instance.UpdateSwitchableObjects(phase);
    }

    public bool IsDay {
        get {
            return phase == DayNightPhase.DAY;
        }
    }

    public bool IsNight {
        get {
            return phase == DayNightPhase.NIGHT;
        }
    }

    private int NumberOfWavesInCurrentDay() {
        return dayCounter * 2;
    }

    private int NumberOfZombiesInWave() {
        return dayCounter * 1 + Random.Range(0, dayCounter);
    }
}
