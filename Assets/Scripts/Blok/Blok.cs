using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blok : MonoBehaviour, IDamageable<float> {

    
    public string blockName = "BlokXYZ";

    private HitpointsController hpControl;
    private BlockTargets blockTargets;

    private void Start()
    {
        hpControl = GetComponent<HitpointsController>();
        blockTargets = GetComponent<BlockTargets>();
        hpControl.hpBar.SetBarName(blockName);
        hpControl.onMinimumReached += DestroyBlok;
    }

    public void Damage (float damage) {
        hpControl.DescreaseValue(damage);
    }

    public bool Dead () {
        return hpControl.GetCurrentValue() <= hpControl.minValue;
    }

    public Transform GetRandomTarget() {
        if(!Dead()) {
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
            gameObject.SetActive(false);
            MessageController.Instance.AddMessage(blockName + " was destroyed!!!", 2f, Color.cyan);
            MainObjectManager.Instance.bloky.Remove(gameObject);
        }
    }

}
