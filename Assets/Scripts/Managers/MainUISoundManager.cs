using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUISoundManager : MonoBehaviourSingleton<MainUISoundManager> {

    [Tooltip("Children GameObject that contains a BlopSound")]
    public GameObject BlopSound;
    public GameObject ClickButtonSound;
    public GameObject MainSound;
    public GameObject DaySound;
    public GameObject NightSound;

    private AudioSource blopAS;
    private AudioSource clickAS;
    private AudioSource mainAS;
    private AudioSource dayAS;
    private AudioSource nightAS;

    private void Start()
    {
        blopAS = BlopSound.GetComponent<AudioSource>();
        clickAS = ClickButtonSound.GetComponent<AudioSource>();
        mainAS = MainSound.GetComponent<AudioSource>();
        dayAS = DaySound.GetComponent<AudioSource>();
        nightAS = NightSound.GetComponent<AudioSource>();
    }

    public void PlayBlop() 
    {
        blopAS.Play();
    }

    public void PlayClickButton()
    {
        clickAS.Play();
    }

    public void PlayMain()
    {
        mainAS.Play();
        mainAS.loop = true;
    }

    public void PlayDay()
    {
        dayAS.Play();
    }

    public void PlayNight()
    {
        nightAS.Play();
    }

    public void StopMain()
    {
        mainAS.Stop();
    }
}
