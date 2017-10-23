using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class BarListController : MonoBehaviour {

    private int index;
    private float spacing;
    public MessageController messageController;

    private void Start()
    {
        RecalculateHeight();
        spacing = GetComponent<VerticalLayoutGroup>().spacing;
    }

    public void EnableBar(GameObject bar, string message = null)
    {
        bar.SetActive(true);
        bar.GetComponent<Animator>().SetBool("active", true);
        RecalculateHeight();
        if(message != null && messageController != null)
        {
            messageController.AddMessage(message);
        }
    }

    public void DisableBar(GameObject bar)
    {
        bar.GetComponent<Animator>().SetBool("active", false);
        StartCoroutine(DisableBar(bar, bar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
    }

    private IEnumerator DisableBar(GameObject bar, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        bar.SetActive(false);
        RecalculateHeight();
    }

    public void RecalculateHeight()
    {
        float recalculatedHeight = CalculateNewHeight();
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x, recalculatedHeight);
    }

    public void EnableBar(int index, string message = null)
    {
        EnableBar(transform.GetChild(index).gameObject, message);
    }

    public void DisableBar(int index)
    {
        DisableBar(transform.GetChild(index).gameObject);
    }

    //TODO dummy function - remove
    public void ToggleNext(bool withDemoMessage)
    {
       
        if(index < 12)
        {
            if (withDemoMessage)
            {
                EnableBar(index, transform.GetChild(index).gameObject.GetComponent<BarController>().barName + " needs your aid!!!");
                index++;
            }
            else { 
                EnableBar(index++);
            }
        }
    }
    //TODO dummy function - remove
    public void RemoveLast()
    {
        if (index > 0)
        {
            DisableBar((index--)-1);
        }
    }

    //TODO dummy function - remove
    public void DamageLastAdded(float damage)
    {
        if (index > 0)
        {
            DamageBar(index - 1, damage);

        }
    }

    private void DamageBar(int index, float damage)
    {
        DamageBar(transform.GetChild(index).gameObject, damage);
    }

    
    private void DamageBar(GameObject bar, float damage)
    {
        messageController.AddMessage(bar.GetComponent<BarController>().barName + " took " + damage + "damage!");
        bar.GetComponent<BarController>().DescreaseValue(damage);
    }

    private float CalculateNewHeight()
    {
        float newHeight = 0f;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform rt = transform.GetChild(i).GetComponent<RectTransform>();
            if (rt != null && rt.gameObject.activeInHierarchy)
            {
                newHeight += rt.sizeDelta.y + spacing;
            }
        }
        return newHeight;
    }
}
