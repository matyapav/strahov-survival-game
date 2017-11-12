using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuController : MonoBehaviour {

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
        InputHandler.Instance.Controls_Build.AddListener(Toggle);
    }

    public void Toggle()
    {
        animator.SetBool("active", !opened);
        opened = !opened;
        if (opened) { 
            scrollBar.value = 0;
        }
    }
}
