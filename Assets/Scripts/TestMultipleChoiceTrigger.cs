using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestMultipleChoiceTrigger : MonoBehaviour, IPointerClickHandler {

    public MultipleChoiceController multipleChoice;
    public Sprite backGroundImage;
    private MultipleChoiceController multipleChoiceControllerInstance;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
    }

    private void Start()
    {
        multipleChoiceControllerInstance = multipleChoice.GetInstance(transform)
                                                         .SetNumberOfChoices(4)
                                                         .SetBackgroundImage(backGroundImage)
                                                         .SetTextToAll("")
                                                         .SetScale(0.5f, 0.5f);
        multipleChoiceControllerInstance.onFirstButtonDown += Repair;
        multipleChoiceControllerInstance.onSecondButtonDown += Move;
        multipleChoiceControllerInstance.onThirdButtonDown += Upgrade;
        multipleChoiceControllerInstance.onFourthButtonDown += Destroy;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
            {
                if (hit.collider.gameObject.name.Equals(gameObject.name)) { //hit only this cube
                    multipleChoiceControllerInstance.ShowPanel(transform.position);
                } else
                {
                    multipleChoiceControllerInstance.HidePanel();
                }
            }
        }
	}

    void Repair()
    {
        //just change cube to blue
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = new Color(0f, 0f, 1f);
    }

    void Move()
    {
        //just move it forward by one
        transform.position = transform.position + Vector3.forward;
    }

    void Upgrade()
    {
        //just change cube to green
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = new Color(0f, 1f, 0f);
    }

    void Destroy()
    {
        //make cube inactive
        gameObject.SetActive(false);
    }
}
