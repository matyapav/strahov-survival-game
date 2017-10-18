using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugImagesVisibilityManager : MonoBehaviour {

    public List<GameObject> toBeVisibleInDebug;
    public bool isDebug = false;
	
	void Start()
    {
        if(isDebug) { 
            foreach (GameObject obj in toBeVisibleInDebug)
            {
                foreach (Image img in obj.GetComponentsInChildren<Image>())
                {
                    img.enabled = true;
                    Color color = img.color;
                    color.a = 1f;
                    img.color = color;
                }
            }
        }
    }
}
