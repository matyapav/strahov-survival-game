using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BarController : MonoBehaviour {

    public BarListController barListController;

    [Tooltip("Bar name text component")]
    public Text barNameText;
    [Tooltip("Bar rendering image")]
    public Image barValueImage;
    [Tooltip("After how many seconds without any change should the bar hide.")]
    public float secondsToHide = 3f;


    public delegate void OnMinimumReached();

    private string barName;
    private float maxValue;
    private float minValue;
    private float currentValue;

    private void Start()
    {
        barNameText.text = barName;
    }

    public void SetMaxValue(float maxVal)
    {
        this.maxValue = maxVal;
    }

    public void SetMinValue(float minVal)
    {
        this.minValue = minVal;
    }

    public void SetValue(float newValue)
    {
        currentValue = newValue;
        RenderValue();
    }

    public void SetBarName(string text)
    {
        barName = text;
        barNameText.text = text;
    }

    private void RenderValue()
    {
        float fillAmount = currentValue / maxValue;
        fillAmount = fillAmount > 1f ? 1f : fillAmount;
        fillAmount = fillAmount < 0f ? 0f : fillAmount;
        barValueImage.fillAmount = fillAmount;
    }

    public void EnableBar(string message = null)
    {
        CancelInvoke("DisableBar");
        if (!IsEnabled())
        {
            this.gameObject.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("active", true);
            barListController.RecalculateHeight();
            if(message != null)
            {
                MessageController.Instance.AddMessage(message, 2f);
            }
        }
        Invoke("DisableBar", secondsToHide);
    }

    public void DisableBar()
    {
        if (IsEnabled()) { 
            this.gameObject.GetComponent<Animator>().SetBool("active", false);
            StartCoroutine(DisableBarAfterSeconds(this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
        }
    }

    private IEnumerator DisableBarAfterSeconds(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.gameObject.SetActive(false);
        barListController.RecalculateHeight();
    }

    private bool IsEnabled()
    {
        return this.gameObject.activeInHierarchy;
    }

}
