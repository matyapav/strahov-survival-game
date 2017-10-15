using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceControllerBuilder : MonoBehaviour {

    private int numberOfChoices = 4;

    private string firstButtonText;
    private string secondButtonText;
    private string thirdButtonText;
    private string fourthButtonText;

    private Sprite firstBackgroundImage;
    private Sprite secondBackgroundImage;
    private Sprite thirdBackgroundImage;
    private Sprite fourthBackgroundImage;
    private bool allBackgrounsSame;

    private Color firstBackgroundColor;
    private Color secondBackgroundColor;
    private Color thirdBackgroundColor;
    private Color fourthBackgroundColor;
    private bool allBackgroundColorsSame;

    private float scaleX;
    private float scaleY;

    public GameObject oneChoice;
    public GameObject twoChoices;
    public GameObject threeChoices;
    public GameObject fourChoices;

    private List<MultipleChoiceController.OnButtonDownDelegate> firstButtonActions;
    private List<MultipleChoiceController.OnButtonDownDelegate> secondButtonActions;
    private List<MultipleChoiceController.OnButtonDownDelegate> thirdButtonActions;
    private List<MultipleChoiceController.OnButtonDownDelegate> fourthButtonActions;

    /// <summary>
    /// Adds action to button ONE
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public MultipleChoiceControllerBuilder AddOnFirstButtonDownAction(MultipleChoiceController.OnButtonDownDelegate func)
    {
        if(firstButtonActions == null)
        {
            firstButtonActions = new List<MultipleChoiceController.OnButtonDownDelegate>();
        }
        firstButtonActions.Add(func);
        return this;
    }

    /// <summary>
    /// Adds action to button TWO
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public MultipleChoiceControllerBuilder AddOnSecondButtonDownAction(MultipleChoiceController.OnButtonDownDelegate func)
    {
        if (secondButtonActions == null)
        {
            secondButtonActions = new List<MultipleChoiceController.OnButtonDownDelegate>();
        }
        secondButtonActions.Add(func);
        return this;
    }

    /// <summary>
    /// Adds action to button THREE
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public MultipleChoiceControllerBuilder AddOnThirdButtonDownAction(MultipleChoiceController.OnButtonDownDelegate func)
    {
        if (thirdButtonActions == null)
        {
            thirdButtonActions = new List<MultipleChoiceController.OnButtonDownDelegate>();
        }
        thirdButtonActions.Add(func);
        return this;
    }

    /// <summary>
    /// Adds action to button four
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public MultipleChoiceControllerBuilder AddOnFourthButtonDownAction(MultipleChoiceController.OnButtonDownDelegate func)
    {
        if (fourthButtonActions == null)
        {
            fourthButtonActions = new List<MultipleChoiceController.OnButtonDownDelegate>();
        }
        fourthButtonActions.Add(func);
        return this;
    }

    /// <summary>
    /// Sets background color to all buttons in multichoice panel. Sets the same image for all buttons.
    /// </summary>
    /// <param name="color"></param>
    public MultipleChoiceControllerBuilder SetBackgroundColorToAllButtons(Color color)
    {
        firstBackgroundColor = color;
        secondBackgroundColor = color;
        thirdBackgroundColor = color;
        fourthBackgroundColor = color;
        allBackgroundColorsSame = true;
        return this;
    }

    /// <summary>
    /// Sets background image to a button with given number. 
    /// For 4 choices - 1 = left, 2 = top, 3 = right, 4 = bottom
    /// For 3 choices - 1 = left, 2 = top, 3 = right
    /// For 2 choices - 1 = bottom left, 2 = top right
    /// For 1 choice - use 1;
    /// </summary>
    /// <param name="color"></param>
    /// <param name="buttonNumber"></param>
    public MultipleChoiceControllerBuilder SetBackgroundColorToButton(Color color, int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 1:
                firstBackgroundColor = color;
                break;
            case 2:
                secondBackgroundColor = color;
                break;
            case 3:
                thirdBackgroundColor = color;
                break;
            case 4:
                fourthBackgroundColor = color;
                break;
            default:
                //TODO handle default case;
                break;
        }
        allBackgroundColorsSame = false;
        return this;
    }

    /// <summary>
    /// Sets background image to all buttons in multichoice panel. Sets the same image for all buttons.
    /// </summary>
    /// <param name="image"></param>
    public MultipleChoiceControllerBuilder SetBackgroundImageToAllButtons(Sprite image)
    {
        firstBackgroundImage = image;
        secondBackgroundImage = image;
        thirdBackgroundImage = image;
        fourthBackgroundImage = image;
        allBackgrounsSame = true;
        return this;
    }

    /// <summary>
    /// Sets background image to a button with given number. 
    /// For 4 choices - 1 = left, 2 = top, 3 = right, 4 = bottom
    /// For 3 choices - 1 = left, 2 = top, 3 = right
    /// For 2 choices - 1 = bottom left, 2 = top right
    /// For 1 choice - use 1;
    /// </summary>
    /// <param name="text"></param>
    /// <param name="buttonNumber"></param>
    public MultipleChoiceControllerBuilder SetBackgroundImageToButton(Sprite image, int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 1:
                firstBackgroundImage = image;
                break;
            case 2:
                secondBackgroundImage = image;
                break;
            case 3:
                thirdBackgroundImage = image;
                break;
            case 4:
                fourthBackgroundImage = image;
                break;
            default:
                //TODO handle default case;
                break;
        }
        allBackgrounsSame = false;
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
    public MultipleChoiceControllerBuilder SetTextToButton(string text, int buttonNumber)
    {
        switch (buttonNumber) {
            case 1:
                firstButtonText = text;
                break;
            case 2:
                secondButtonText = text;
                break;
            case 3:
                thirdButtonText = text;
                break;
            case 4:
                fourthButtonText = text;
                break;
            default:
                //TODO handle default case;
                break;
        }

        return this;
    }

    /// <summary>
    /// Sets same text to all buttons in multiple choice
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public MultipleChoiceControllerBuilder SetTextToAllButtons(string text)
    {
        firstButtonText = text;
        secondButtonText = text;
        thirdButtonText = text;
        fourthButtonText = text;
        return this;
    }

    /// <summary>
    /// Sets number of choices wanted. Layout of multiple choice is than selected by this parameter.
    /// </summary>
    /// <param name="numberOfChoices"></param>
    public MultipleChoiceControllerBuilder SetNumberOfChoices(int numberOfChoices)
    {
        this.numberOfChoices = numberOfChoices;
        return this;
    }

    /// <summary>
    /// Sets scale of multiple choice panel visualzation
    /// </summary>
    /// <param name="scaleX"></param>
    /// <param name="scaleY"></param>
    /// <returns></returns>
    public MultipleChoiceControllerBuilder SetScale(float scaleX, float scaleY)
    {
        this.scaleX = scaleX;
        this.scaleY = scaleY;
        return this;
    }

    /// <summary>
    /// Build multiple choice and returns its controller
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public MultipleChoiceController Build(Transform parent = null)
    {
        //get container
        GameObject container = GameObject.FindWithTag("MultipleChoicesPanelContainer");
        if(container == null)
        {
            container = Instantiate(transform.GetChild(0).gameObject); //first child must be container with canvas
        }

        //panel build with proper hierarchy
        GameObject controllerObject = CreateControllerObject();
        GameObject panelVisualization = BuildPanelVisualization(numberOfChoices);
        controllerObject.transform.SetParent(container.transform);
        controllerObject.transform.localScale = new Vector3(scaleX, scaleY, 1f);
        panelVisualization.transform.SetParent(controllerObject.transform);

        //controller setup
        MultipleChoiceController controller = controllerObject.GetComponent<MultipleChoiceController>();
        controller.SetPanelVisualization(panelVisualization);
        AddButtonActions(controller);

        return controller;
    }


    // UNDER HOOD FUNCTIONS //////////////////////////////////////////////////////////////////////////////
    private void AddButtonActions(MultipleChoiceController controllerObject)
    {
        if (firstButtonActions != null)
        {
            foreach (MultipleChoiceController.OnButtonDownDelegate func in firstButtonActions)
            {
                controllerObject.onFirstButtonDown += func;
            }
        }
        if (secondButtonActions != null)
        {
            foreach (MultipleChoiceController.OnButtonDownDelegate func in secondButtonActions)
            {
                controllerObject.onSecondButtonDown += func;
            }
        }
        if (thirdButtonActions != null)
        {
            foreach (MultipleChoiceController.OnButtonDownDelegate func in thirdButtonActions)
            {
                controllerObject.onThirdButtonDown += func;
            }
        }
        if (fourthButtonActions != null)
        {
            foreach (MultipleChoiceController.OnButtonDownDelegate func in fourthButtonActions)
            {
                controllerObject.onFourthButtonDown += func;
            }
        }
        ClearControllerActions();
    }

    private GameObject GetObjectAccordingToNumberOfChoices(int numberOfChoices)
    {
        switch (numberOfChoices)
        {
            case 1:
                return oneChoice;
            case 2:
                return twoChoices;
            case 3:
                return threeChoices;
            case 4:
                return fourChoices;
            default:
                return null;
        }
    }

    private GameObject BuildPanelVisualization(int numberOfChoices)
    {
        GameObject obj = Instantiate(GetObjectAccordingToNumberOfChoices(numberOfChoices));
        if (allBackgrounsSame) { 
            SetBackgroundToAll(firstBackgroundImage, obj);
        }else
        {
            SetBackgroundImage(obj, firstBackgroundImage, 1);
            SetBackgroundImage(obj, secondBackgroundImage, 2);
            SetBackgroundImage(obj, thirdBackgroundImage, 3);
            SetBackgroundImage(obj, fourthBackgroundImage, 4);
        }
        if (allBackgroundColorsSame)
        {
            SetBackgroundColorToAll(firstBackgroundColor, obj);
        }
        else
        {
            SetBackgroundColor(obj, firstBackgroundColor, 1);
            SetBackgroundColor(obj, secondBackgroundColor, 2);
            SetBackgroundColor(obj, thirdBackgroundColor, 3);
            SetBackgroundColor(obj, fourthBackgroundColor, 4);
        }
        SetText(obj, firstButtonText, 1);
        SetText(obj, secondButtonText, 2);
        SetText(obj, thirdButtonText, 3);
        SetText(obj, fourthButtonText, 4);
        return obj;
    }

    private void SetText(GameObject panel, string text, int buttonNumber)
    {
        Button btn = GetProperButton(panel, buttonNumber);
        if(btn != null)
        {
            btn.GetComponentInChildren<Text>().text = text;
        }
    }

    private void SetBackgroundToAll(Sprite backgroundImage, GameObject panelVisualization)
    {
        if (backgroundImage != null)
        {
            foreach (Image img in panelVisualization.GetComponentsInChildren<Image>(true))
            {
                img.sprite = backgroundImage;
            }
        }
    }

    private void SetBackgroundImage(GameObject panelVisualization, Sprite image, int buttonNumber)
    {
        Button btn = GetProperButton(panelVisualization, buttonNumber);
        if(btn != null && image != null)
        {
            btn.GetComponentInChildren<Image>().sprite = image;
        }
    }

    private void SetBackgroundColorToAll(Color backgroundColor, GameObject panelVisualization)
    {
        if (backgroundColor != null)
        {
            foreach (Image img in panelVisualization.GetComponentsInChildren<Image>(true))
            {
                img.color = backgroundColor;
            }
        }
    }

    private void SetBackgroundColor(GameObject panelVisualization, Color backgroundColor, int buttonNumber)
    {
        Button btn = GetProperButton(panelVisualization, buttonNumber);
        if (btn != null && backgroundColor != Color.clear)
        {
            btn.GetComponentInChildren<Image>().color = backgroundColor;
        }
    }

    private Button GetProperButton(GameObject panelVisualization, int buttonNumber)
    {
        
        foreach (Button button in panelVisualization.GetComponentsInChildren<Button>(true))
        {
            switch (buttonNumber)
            {
                case 1:
                    if (button.gameObject.name.Equals("A"))
                    {
                        return button;
                    }
                    break;
                case 2:
                    if (button.gameObject.name.Equals("B"))
                    {
                        return button;
                    }
                    break;
                case 3:
                    if (button.gameObject.name.Equals("C"))
                    {
                        return button;
                    }
                    break;
                case 4:
                    if (button.gameObject.name.Equals("D"))
                    {
                        return button;
                    }
                    break;
                default:
                    break;
            }
        }
        return null;
    }

    private GameObject CreateControllerObject()
    {
        GameObject controllerObject = new GameObject();
        controllerObject.name = "ControllerObject";
        controllerObject.AddComponent<MultipleChoiceController>();
        return controllerObject;
    }

    private void ClearControllerActions()
    {
        ClearList(firstButtonActions);
        ClearList(secondButtonActions);
        ClearList(thirdButtonActions);
        ClearList(fourthButtonActions);
    }

    private void ClearList(List<MultipleChoiceController.OnButtonDownDelegate> list)
    {
        if(list != null)
        {
            list.Clear();
        }
    }
}
