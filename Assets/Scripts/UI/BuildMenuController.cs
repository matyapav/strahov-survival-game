using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuController : MonoBehaviourSingleton<BuildMenuController> {

    public Scrollbar scrollBar;
    private Animator animator;
    private bool opened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Attach the listener to the controls
        // Done directly
        // InputHandler.Instance.Controls_Build.AddListener(Toggle);
    }

    public void Toggle() {
        Debug.Log("Toggle");
        if (DayNightController.Instance.switching == false) {
            if (animator != null)
            {
                Debug.Log("Actually toggling");
                // Sound
                MainUISoundManager.Instance.PlaySound("scroll");

                // Update the animator
                animator.SetBool("active", !opened);
                opened = !opened;
                if (opened)
                {
                    scrollBar.value = 0;
                }
            } 
        } else {
            Debug.Log("Cannot toggle build menu while switching");   
        }
    }

    public void Hide() {
        if(opened) {
            Debug.Log("Hiding");
            animator.SetBool("active", false);
            opened = false;
            scrollBar.value = 0;
        }
    }
}
