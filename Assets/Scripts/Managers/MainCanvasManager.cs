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


    private void OnEnable()
    {
        // Listener to the escape key
        InputHandler.Instance.Controls_Exit.AddListener(OnExitPressed);

        // Listener to the OpenBlackMarketMenu
        MainEventManager.Instance.OnBlackMarketClicked.AddListener(ShowBlackMarketMenu);
    }

    private void OnExitPressed() {
        if (BlackMarketMenu.activeSelf) {
            HideBlackMarketMenu();
        }
        else if (PauseMenu.activeSelf) {
            HidePauseMenu();
        }
        else if(!PauseMenu.activeSelf) {
            ShowPauseMenu();
        }
    }

    private void ShowBlackMarketMenu() {
        // Check if the pause menu is active. If it is do not open the menu.
        if(!PauseMenu.activeSelf) {
            BlackMarketMenu.SetActive(true);
            PauseTimescale();
        }
    }

    private void HideBlackMarketMenu() {
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
}
