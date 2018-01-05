using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Manages enablind and disabling the UI elements in the canvas
public class MainCanvasManager : MonoBehaviourSingleton<MainCanvasManager> {

    // Contains references to all its children just for convience
    public GameObject PauseMenu;
    public GameObject Money;
    public GameObject TimeText;
    public GameObject WaveText;
    public GameObject BlackMarketMenu;
    public GameObject BuildMenu;
    public GameObject BuildingsInfoMenu;
    public GameObject EndDay;
    public GameObject ReloadUI;
    public GameObject GameOver;
    public Text GameOverText;

    private void Start()
    {
        PauseMenu.SetActive(false);
        Money.SetActive(true);
        TimeText.SetActive(false);
        WaveText.SetActive(true);
        BuildMenu.SetActive(true);
        BlackMarketMenu.SetActive(false);
        BuildingsInfoMenu.SetActive(false);
        GameOver.SetActive(false);
    }

    private void OnEnable()
    {
        // Listener to the escape key
        InputHandler.Instance.Controls_Exit.AddListener(OnExitPressed);

        // Listener to the OpenBlackMarketMenu
        MainEventManager.Instance.OnBlackMarketClicked.AddListener(ShowBlackMarketMenu);
    }

    private void OnExitPressed() {
        if (BuildingsInfoMenu.activeInHierarchy)
        {
            HideBuildingsInfo();
        }
        else if (BlackMarketMenu.activeInHierarchy) {
            HideBlackMarketMenu();
        }
        else if (PauseMenu.activeInHierarchy) {
            HidePauseMenu();
        }
        else if(!PauseMenu.activeInHierarchy) {
            ShowPauseMenu();
        }
    }

    private void ShowBlackMarketMenu() {
        // Check if the pause menu is active. If it is do not open the menu.
        if(!PauseMenu.activeInHierarchy) {
            BlackMarketMenu.SetActive(true);
            PauseTimescale();
        }
    }

    public void ShowBuildingsInfo() {
        // Check if the pause menu is active. If it is do not open the menu.
        if (!PauseMenu.activeInHierarchy) {
            BuildingsInfoMenu.SetActive(true);
            PauseTimescale();
        }
    }

    public void HideBuildingsInfo() {
        BuildingsInfoMenu.SetActive(false);
        ResumeTimescale();
    }

    public void HideBuildMenu() {
        BuildMenu.SetActive(false);
    }

    public void HideEndDayButton() {
        EndDay.SetActive(false);
    }

    public void ShowEndDayButton() {
        EndDay.SetActive(true);
    }

    public void ShowBuildMenu() {
        BuildMenu.SetActive(true);
    }

    public void HideBlackMarketMenu() {
        BlackMarketMenu.SetActive(false);
        ResumeTimescale();
    }

    private void ShowPauseMenu() {
        PauseMenu.SetActive(true);
        PauseTimescale();
    }

    private void HidePauseMenu() {
        PauseMenu.SetActive(false);
        ResumeTimescale();
    }

    public void PauseTimescale() {
        Time.timeScale = 0f;
    }

    public void ResumeTimescale() {
        Time.timeScale = 1f;
    }

    public void HideReloadUI() {
        ReloadUI.SetActive(false);
    }

    public void ShowReloadUI() {
        ReloadUI.SetActive(true);
    }

    public void ShowGameOver() {
        MainUISoundManager.Instance.PlaySound("game_over");
        GameOver.SetActive(true);
        GameOverText.text = "There is no life without beer. Wanna try again?\nYou have fought bravely and survived for " 
            + DayNightController.Instance.GetDayCount() +
            " days. ";
        PauseTimescale();
    }
}
