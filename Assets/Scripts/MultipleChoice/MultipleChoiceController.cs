using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultipleChoiceController : MonoBehaviour
{
    public delegate void OnButtonDownDelegate();
    public event OnButtonDownDelegate onFirstButtonDown;
    public event OnButtonDownDelegate onSecondButtonDown;
    public event OnButtonDownDelegate onThirdButtonDown;
    public event OnButtonDownDelegate onFourthButtonDown;

    public delegate void OnPanelVisibilityChanged();
    public event OnPanelVisibilityChanged onPanelShow;
    public event OnPanelVisibilityChanged onPanelHide;

    public bool MultipleChoiceVisible
    {
        get
        {
            return multipleChoiceVisible;
        }
    }

    private bool multipleChoiceVisible = false;
    private string animatorParameterName = "visible";
    private Vector3 screenPos;
    private Vector3 worldPos;
    private GameObject panelVisualization;

    private void Start()
    {
        foreach (Button btn in GetComponentsInChildren<Button>(true))
        {
            btn.onClick.AddListener(HidePanel);
            btn.onClick.AddListener(() => {
                OnButtonDown(btn);
            });
        }
    }

    private void OnButtonDown(Button button)
    {
        switch (button.gameObject.name)
        {
            case "A":
                onFirstButtonDown();
                break;
            case "B":
                onSecondButtonDown();
                break;
            case "C":
                onThirdButtonDown();
                break;
            case "D":
                onFourthButtonDown();
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if(multipleChoiceVisible && panelVisualization.transform.position != worldPos)
        {
            panelVisualization.transform.position = Camera.main.WorldToScreenPoint(worldPos);
        }
    }

    //API

    /// <summary>
    /// Shows proper multichoice panel on specified world position
    /// </summary>
    /// <param name="position"></param>
    public void ShowPanel(Vector3 position)
    {
        Debug.Log("showing panel");
        TogglePanel(true, Camera.main.WorldToScreenPoint(position));
        worldPos = position;
        if(onPanelShow != null){
            onPanelShow.Invoke();
        }
    }

    /// <summary>
    /// Hides previosly shown multichoice panel
    /// </summary>
    /// <param name="position"></param>
    public void HidePanel() {
        TogglePanel(false, Camera.main.WorldToScreenPoint(worldPos));
        if(onPanelHide != null){
            onPanelHide.Invoke();
        }
    }

    // UNDER HOOD
    private void TogglePanel(bool show, Vector3 position)
    {
        if (show != multipleChoiceVisible && panelVisualization != null)
        {
            panelVisualization.SetActive(true);
            panelVisualization.transform.position = position;
            panelVisualization.GetComponent<Animator>().SetBool(animatorParameterName, show);
            multipleChoiceVisible = show;
        }
    }

    public void SetPanelVisualization(GameObject panelVisualization)
    {
        this.panelVisualization = panelVisualization;
    }


}