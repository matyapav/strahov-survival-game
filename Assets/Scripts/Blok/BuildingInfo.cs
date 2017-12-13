using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour {

    public HitpointsController hitpointsController;
    private Image buildingInfoImage;

    private void Start()
    {
        buildingInfoImage = GetComponent<Image>();
    }
    // Use this for initialization
    void OnEnable()
    {
        buildingInfoImage = GetComponent<Image>();
        if (hitpointsController.minimumReached)
        {
            buildingInfoImage.color = Color.grey;
        }
        else if (hitpointsController.IsUnderAttack)
        {
            buildingInfoImage.color = Color.red;
        }
    }
}
