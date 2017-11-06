using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuItem : MonoBehaviour {

    public Text priceText;
    public Button buyButton;
    public float price;
    public GameObject obstaclePrefab;

    private void Start()
    {
        if(price > 1000)
        {
            priceText.text = price / 1000 + "K";
        }
        else {
            priceText.text = price.ToString();
        }
        buyButton.onClick.AddListener(PushToObstaclePlacer);
    }

    public void PushToObstaclePlacer()
    {
        if (CurrencyController.Instance.CanDecrease(price))
        {
            ObstaclePlacer.Instance.SetObstacle(obstaclePrefab, price);
        }
        else
        {
            MessageController.Instance.AddMessage("You cannot afford this!", 3f, Color.red);
        }
    }
}
