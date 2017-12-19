using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blok : MonoBehaviour, IDamageable<float> {

    
    public string blockName = "BlokX";

    private HitpointsController hpControl;

    private void Start()
    {
        hpControl = GetComponent<HitpointsController>();
        hpControl.hpBar.SetBarName(blockName);
        hpControl.onMinimumReached += DestroyBlok;
    }

    public void Damage (float damage) {
        hpControl.DescreaseValue(damage);
    }

    public bool Dead () {
        return hpControl.GetCurrentValue() <= hpControl.minValue;
    }

    public void DestroyBlok()
    {
        if (gameObject.activeInHierarchy) { 
            gameObject.SetActive(false);
            MessageController.Instance.AddMessage(blockName + " was destroyed!!!", 2f, Color.cyan);
            MainObjectManager.Instance.bloky.Remove(gameObject); //remove block from possible targets
        }
    }

}
