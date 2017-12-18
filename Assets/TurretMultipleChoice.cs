using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMultipleChoice : MultipleChoiceTrigger {

	// Use this for initialization
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
	}
    private void HideRange()
    {
        this.GetComponent<TurretController>().rangeCylinder.SetActive(false);
    }

    private void ShowRange()
    {
        this.GetComponent<TurretController>().rangeCylinder.SetActive(true);
    }

    void Repair()
    {
        // just change cube to blue
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = new Color(0f, 0f, 1f);

        MainUISoundManager.Instance.PlaySound("blop");
    }

    void Move()
    {
		ShowRange();
        // just move it 
        ObstaclePlacer.Instance.SetObstacle(gameObject, 0, true);

        MainUISoundManager.Instance.PlaySound("blop");
    }

    void Upgrade()
    {
        // just change cube to green
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.color = new Color(0f, 1f, 0f);

        MainUISoundManager.Instance.PlaySound("blop");
    }

    void Destroy()
    {
        Destroy(gameObject);

        MainUISoundManager.Instance.PlaySound("blop");
    }
}
