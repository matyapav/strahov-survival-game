using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMarketButton : MonoBehaviour {

    public HitpointsController hitpointsController;
    public Image blackMarketBar;
    public BlackMarketMenuController blackMarketMenuController;

    private float refillAmount;
    private float pricePerUnit;

    private void Start()
    {
        refillAmount = blackMarketMenuController.refillAmount;
        pricePerUnit = blackMarketMenuController.pricePerUnit;
    }
    // Use this for initialization
    void OnEnable () {
        blackMarketBar.fillAmount = hitpointsController.hpBar.barValueImage.fillAmount;
	}

    public void Refill()
    {
        if(CurrencyController.Instance.CanDecrease(refillAmount * pricePerUnit) && hitpointsController.CanIncrease(refillAmount))
        {
            hitpointsController.IncreaseValue(refillAmount);
            CurrencyController.Instance.DescreaseValue(refillAmount * pricePerUnit); 
            blackMarketBar.fillAmount = hitpointsController.hpBar.barValueImage.fillAmount;
        }
    }
	
}
