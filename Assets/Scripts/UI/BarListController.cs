using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class BarListController : MonoBehaviour {

    private float spacing;

    private void Start()
    {
        RecalculateHeight();
        spacing = GetComponent<VerticalLayoutGroup>().spacing;
    }

    public void RecalculateHeight()
    {
        float recalculatedHeight = CalculateNewHeight();
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x, recalculatedHeight);
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
