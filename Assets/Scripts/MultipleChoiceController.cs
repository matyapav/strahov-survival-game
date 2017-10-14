using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceController : MonoBehaviour
{
    public GameObject oneChoice;
    public GameObject twoChoices;
    public GameObject threeChoices;
    public GameObject fourChoices;

    public bool MultipleChoiceVisible
    {
        get
        {
            return multipleChoiceVisible;
        }
    }

    public delegate void OnButtonDownDelegate();
    public event OnButtonDownDelegate onFirstButtonDown;
    public event OnButtonDownDelegate onSecondButtonDown;
    public event OnButtonDownDelegate onThirdButtonDown;
    public event OnButtonDownDelegate onFourthButtonDown;

    private int numberOfChoices = 4;
    private bool multipleChoiceVisible = false;
    private string animatorParameterName = "visible";
    private Vector3 pos;
    private GameObject actualChoicesObject;

    private void Start()
    {
        foreach(Button btn in GetComponentsInChildren<Button>(true))
        {
            btn.onClick.AddListener(HidePanel);
            btn.onClick.AddListener(() => {
                OnButtonDown(btn);
            });
        }   
    }

    private void Update()
    {
        if(multipleChoiceVisible)
        {
            actualChoicesObject.transform.position = Camera.main.WorldToScreenPoint(pos);
        }
    }

    //API

    public MultipleChoiceController GetInstance(Transform parent)
    {
        GameObject obj = Instantiate(gameObject);
        obj.transform.SetParent(parent);
        return obj.GetComponent<MultipleChoiceController>();
    }
    /// <summary>
    /// Shows proper multichoice panel on specified world position
    /// </summary>
    /// <param name="position"></param>
    public void ShowPanel(Vector3 position)
    {
        TogglePanel(true, position);
        pos = position;
        
    }

    /// <summary>
    /// Hides previosly shown multichoice panel
    /// </summary>
    /// <param name="position"></param>
    public void HidePanel() {
        TogglePanel(false, Camera.main.WorldToScreenPoint(pos));
    }

    /// <summary>
    /// Sets background image to all buttons in multichoice panel. Sets the same image for all buttons.
    /// </summary>
    /// <param name="image"></param>
    public MultipleChoiceController SetBackgroundImage(Sprite image)
    {
        switch (numberOfChoices)
        {
            case 1:
                SetBackgroundImageToProperPanel(oneChoice, image);
                break;
            case 2:
                SetBackgroundImageToProperPanel(twoChoices, image);
                break;
            case 3:
                SetBackgroundImageToProperPanel(threeChoices, image);
                break;
            case 4:
                SetBackgroundImageToProperPanel(fourChoices, image);
                break;
            default:
                break;
        }
        return this;
    }

    /// <summary>
    /// Sets text to a button with given number. 
    /// For 4 choices - 1 = left, 2 = top, 3 = right, 4 = bottom
    /// For 3 choices - 1 = left, 2 = top, 3 = right
    /// For 2 choices - 1 = bottom left, 2 = top right
    /// For 1 choice - use 1;
    /// </summary>
    /// <param name="text"></param>
    /// <param name="buttonNumber"></param>
    public MultipleChoiceController SetText(string text, int buttonNumber)
    {
        switch (numberOfChoices)
        {
            case 1:
                SetTextToProperPanel(oneChoice, text, buttonNumber);
                break;
            case 2:
                SetTextToProperPanel(twoChoices, text, buttonNumber);
                break;
            case 3:
                SetTextToProperPanel(threeChoices, text, buttonNumber);
                break;
            case 4:
                SetTextToProperPanel(fourChoices, text, buttonNumber);
                break;
            default:
                break;
        }
        return this;
    }

    public MultipleChoiceController SetTextToAll(string text)
    {
        GameObject choice = null;
        switch (numberOfChoices)
        {
            case 1:
                choice = oneChoice;
                break;
            case 2:
                choice = twoChoices;
                break;
            case 3:
                choice = threeChoices;
                break;
            case 4:
                choice = fourChoices;
                break;
            default:
                break;
        }
        if(choice != null) { 
            SetTextToProperPanel(choice, text, 1);
            SetTextToProperPanel(choice, text, 2);
            SetTextToProperPanel(choice, text, 3);
            SetTextToProperPanel(choice, text, 4);
        }
        return this;
    }

    /// <summary>
    /// Sets number of choices wanted. Layout of multiple choice is than selected by this parameter.
    /// </summary>
    /// <param name="numberOfChoices"></param>
    public MultipleChoiceController SetNumberOfChoices(int numberOfChoices)
    {
        this.numberOfChoices = numberOfChoices;
        return this;
    }

    public MultipleChoiceController SetScale(float scaleX, float scaleY)
    {
       transform.GetChild(0).localScale = new Vector3(scaleX, scaleY, 1f);
        return this;
    }

    // UNDER HOOD
    private void TogglePanel(bool show, Vector3 position)
    {
        if (show != multipleChoiceVisible)
        {
            switch (numberOfChoices)
            {
                case 1:
                    ToggleProperMultipleChoiceObject(oneChoice, position, show);
                    break;
                case 2:
                    ToggleProperMultipleChoiceObject(twoChoices, position, show);
                    break;
                case 3:
                    ToggleProperMultipleChoiceObject(threeChoices, position, show);
                    break;
                case 4:
                    ToggleProperMultipleChoiceObject(fourChoices, position, show);
                    break;
                default:
                    break;
            }
            multipleChoiceVisible = show;
        }
    }
    private void OnButtonDown(Button button)
    {
        //TODO think about hardcoded object names
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
    private void SetBackgroundImageToProperPanel(GameObject panel, Sprite image)
    {
        foreach (Image img in panel.GetComponentsInChildren<Image>(true))
        {
            img.sprite = image;
        }
    }
    private void SetTextToProperPanel(GameObject panel, string text, int buttonNumber)
    { //TODO think about hardcoded object names
        foreach (Button button in panel.GetComponentsInChildren<Button>(true))
        {
            switch (buttonNumber)
            {
                case 1:
                    SetTextToProperButton(button, "A", text);
                    break;
                case 2:
                    SetTextToProperButton(button, "B", text);
                    break;
                case 3:
                    SetTextToProperButton(button, "C", text);
                    break;
                case 4:
                    SetTextToProperButton(button, "D", text);
                    break;
                default:
                    break;
            }
        }
    }
    private void SetTextToProperButton(Button button, string expectedButtonName, string text)
    {
        if (button.gameObject.name.Equals(expectedButtonName))
        {
            button.GetComponentInChildren<Text>().text = text;
        }
    }
    private void ToggleProperMultipleChoiceObject(GameObject choice, Vector3 position, bool show)
    {
        choice.SetActive(true);
        choice.transform.position = position;
        choice.GetComponent<Animator>().SetBool(animatorParameterName, show);
        actualChoicesObject = choice;
    }

    

}