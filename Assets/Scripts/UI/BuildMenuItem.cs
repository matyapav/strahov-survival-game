using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuItem : MonoBehaviour {

    public Text priceText;
    public Text objectNameText;
    public Text infoPanelObjectNameText;
    public Text infoPanelInfoText;
    public Image image;
    public Button buyButton;

    public float price;
    public string objectName;
    [TextArea(3, 10)]
    public string infoText;
    public Sprite imageSource;
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
        objectNameText.text = objectName;
        infoPanelObjectNameText.text = objectName;
        infoPanelInfoText.supportRichText = true;
        infoPanelInfoText.text = infoText;
        buyButton.onClick.AddListener(PushToObstaclePlacer);
        image.sprite = imageSource;
    }

    public void PushToObstaclePlacer()
    {
        if (CurrencyController.Instance.CanDecrease(price))
        {
            ObstaclePlacer.Instance.SetObstacle(obstaclePrefab, price);
        }
        else
        {
            MessageController.Instance.AddMessage("You cannot afford "+objectName+" !" , 3f, Color.red);
        }
    }
}
