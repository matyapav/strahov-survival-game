using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMarketButton : MonoBehaviour {

    public HitpointsController hitpointsController;
    public Image blackMarketBar;
    public Button blackMarketBtn;
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
        if(hitpointsController != null && hitpointsController.hpBar != null) {
            blackMarketBar.fillAmount = hitpointsController.hpBar.barValueImage.fillAmount;
            if (blackMarketBar.fillAmount == 0 || blackMarketBar.fillAmount == 1)
            {
                blackMarketBtn.interactable = false;
            } else {
                blackMarketBtn.interactable = true;
            }
        } else {
            blackMarketBar.fillAmount = 0;
            blackMarketBtn.GetComponent<Image>().color = Color.red;
            blackMarketBtn.interactable = false;
        }
    }

    public void Refill()
    {
        if (CurrencyController.Instance.CanDecrease(refillAmount * pricePerUnit) && hitpointsController.CanIncrease(refillAmount))
        {
            hitpointsController.IncreaseValue(refillAmount);
            CurrencyController.Instance.DescreaseValue(refillAmount * pricePerUnit); 
            blackMarketBar.fillAmount = hitpointsController.hpBar.barValueImage.fillAmount;
        } 
    }
	
}
