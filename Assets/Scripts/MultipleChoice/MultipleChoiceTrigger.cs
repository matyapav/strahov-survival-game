using System.Collections;
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

    void Update () {
        if (active && Input.GetMouseButtonDown(1)){
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            {
                if (hit.collider.gameObject == this.gameObject) {
                    multipleChoiceControllerInstance.ShowPanel(transform.position);
                } else
                {
                    multipleChoiceControllerInstance.HidePanel();
                }

                MainUISoundManager.Instance.PlaySound("blop");
            }
        }
	}

    public void Activate(bool a)
    {
        this.active = a;
    }

}
