﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultipleChoiceTrigger : MonoBehaviour {

    public MultipleChoiceControllerBuilder multipleChoiceBuilder;
    public Sprite backGroundImage;
    protected MultipleChoiceController multipleChoiceControllerInstance;
    private bool active = false;
    private bool placing = false;

    protected delegate void TriggerStatusChange();

    void Start () {
		multipleChoiceControllerInstance = multipleChoiceBuilder.SetNumberOfChoices(4)
                                                               .SetBackgroundImageToAllButtons(backGroundImage)
                                                               .SetBackgroundColorToAllButtons(Color.white)
                                                               .SetTextToAllButtons("")
                                                               .SetScale(0.75f, 0.75f)
                                                               .AddOnFirstButtonDownAction(Repair)
                                                               .AddOnSecondButtonDownAction(Move)
                                                               .AddOnThirdButtonDownAction(Upgrade)
                                                               .AddOnFourthButtonDownAction(Destroy)
                                                               .Build(transform);
        PlacingEvents placingEvents = GetComponent<PlacingEvents>();
        if(placingEvents != null) {
            GetComponent<PlacingEvents>().onBeginPlacing.AddListener(() => Activate(false));
            GetComponent<PlacingEvents>().onPlaced.AddListener(() => Activate(true));
        }
	}
    void Update () {
        if (active && Input.GetMouseButtonDown(1)){
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            {
                if (hit.collider.gameObject == this.gameObject) {
                    multipleChoiceControllerInstance.ShowPanel(transform.position);
                    placing = true;
                } else {
                    multipleChoiceControllerInstance.HidePanel();
                    if (placing){
                        MainUISoundManager.Instance.PlaySound("blop");
                        placing = false;
                    }
                }

            }
        }
	}

    public void Activate(bool a)
    {
        this.active = a;
    }

     void Repair()
    {
        // just change cube to blue
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = new Color(0f, 0f, 1f);

        MainUISoundManager.Instance.PlaySound("repair");
        MainUISoundManager.Instance.PlaySound("blop");
    }

    void Move()
    {
        // just move it 
        ObstaclePlacer.Instance.SetObstacle(gameObject, 0, true);

        MainUISoundManager.Instance.PlaySound("blop");
    }

    void Upgrade()
    {
        // just change cube to green
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = new Color(0f, 1f, 0f);

        MainUISoundManager.Instance.PlaySound("upgrade");
        MainUISoundManager.Instance.PlaySound("blop");
    }

    void Destroy()
    {
        Destroy(gameObject);

        MainUISoundManager.Instance.PlaySound("delete_item");
        MainUISoundManager.Instance.PlaySound("blop");
    }

}
