using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUISoundManager : MonoBehaviourSingleton<MainUISoundManager> {

    [Tooltip("Children GameObject that contains a BlopSound")]
    public GameObject BlopSound;
    private AudioSource blopAS;

    private void Start()
    {
        blopAS = BlopSound.GetComponent<AudioSource>();
    }

    public void PlayBlop() 
    {
        blopAS.Play();
    }
}
