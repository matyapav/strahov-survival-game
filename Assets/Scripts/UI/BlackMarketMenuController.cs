using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMarketMenuController : MonoBehaviour
{

    public float refillAmount;
    public float pricePerUnit;
    public Text priceText;

    private void OnEnable()
    {
        if (pricePerUnit > 10000)
        {
            priceText.text = (pricePerUnit / 1000 + "K");
        }
        else
        {
            priceText.text = pricePerUnit.ToString();
        }
    }
}

    
