using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blok : MonoBehaviour {

    
    public string name = "BlokX";

    private HitpointsController hpControl;
    private MessageController messageController;

    private void Start()
    {
        hpControl = GetComponent<HitpointsController>();
        hpControl.hpBar.SetBarName(name);
        hpControl.onMinimumReached += DestroyBlok;
        messageController = GameObject.Find("SceneController").GetComponent<MessageController>(); 
    }


    // Update is called once per frame 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            {
                if (hit.collider.gameObject.name.Equals(gameObject.name))
                { //hit only this cube
                    hpControl.DescreaseValue(5f);
                }
            }
        }
    }

    public void DestroyBlok()
    {
        gameObject.SetActive(false);
        messageController.AddMessage(name + " was destroyed!!!", 2f, Color.cyan);
    }

}
