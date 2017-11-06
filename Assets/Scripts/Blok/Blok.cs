using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blok : MonoBehaviour {

    
    public string blockName = "BlokX";

    private HitpointsController hpControl;

    private void Start()
    {
        hpControl = GetComponent<HitpointsController>();
        hpControl.hpBar.SetBarName(blockName);
        hpControl.onMinimumReached += DestroyBlok;
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
        MessageController.Instance.AddMessage(blockName + " was destroyed!!!", 2f, Color.cyan);
    }

}
