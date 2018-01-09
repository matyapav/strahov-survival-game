using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blok : MonoBehaviour, IDamageable<float> {

    
    public string blockName = "BlokXYZ";

    private HitpointsController hpControl;
    private BlockTargets blockTargets;
    private AudioSource blockAudio;
    public AudioClip swallowing;
    public AudioClip destroyed;

    private void Start()
    {
        hpControl = GetComponent<HitpointsController>();
        blockTargets = GetComponent<BlockTargets>();
        hpControl.hpBar.SetBarName(blockName);
        hpControl.onMinimumReached += DestroyBlok;
		blockAudio = GetComponent<AudioSource>();
    }

    public void Damage (float damage) {
        hpControl.DescreaseValue(damage);
		blockAudio.clip = swallowing;
		blockAudio.Play();
    }

    public bool Dead () {
        return hpControl.GetCurrentValue() <= hpControl.minValue;
    }

    public Transform GetRandomTarget() {
        if(!Dead() && gameObject.activeSelf) {
            Transform rtarget = blockTargets.GetRandomTarget();
            if (rtarget == null) {
                Debug.LogError("Target on the block" + blockName + "is missing.");
            }
            return rtarget;
        }
        return null;
    }

    public void DestroyBlok()
    {
        if (gameObject.activeInHierarchy) {
			blockAudio.clip = destroyed;
			//AudioSource phantomAudio = (AudioSource)GameObject.Instantiate (blockAudio);
			//phantomAudio.clip = destroyed;
            //phantomAudio.Play ();
            //Destroy (phantomAudio, phantomAudio.clip.length);
            MessageController.Instance.AddMessage(blockName + " was destroyed!!!", 2f, Color.cyan);
            MainObjectManager.Instance.bloky.Remove(gameObject);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
