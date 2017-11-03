using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstaclePlacer : MonoBehaviour {

    private GameObject tempObstacle;
    private Color tempObstacleMaterialBackupColor;
    private RaycastHit hit;
    private float place_rotation = 0f;
    private bool canPlace = true;

    public GameObject obstacle_prefab;


	
	// Update is called once per frame
	void Update () {
        if (tempObstacle) {
            Debug.Log(tempObstacle.transform.localScale / 2);
            if (Physics.OverlapBox(tempObstacle.transform.position, tempObstacle.transform.localScale / 2, tempObstacle.transform.rotation, LayerMask.GetMask("Obstacle")).Length > 0)
            {
                //hits another obstacle 
                tempObstacle.GetComponent<Renderer>().material.color = Color.red;
                canPlace = false;
            } else
            {
                canPlace = true;
                tempObstacle.GetComponent<Renderer>().material.color = tempObstacleMaterialBackupColor;
            }
         
        }
        // Rotate the game object when placing
        if (Input.GetKey(KeyCode.Q)) place_rotation -= 2f;
        if (Input.GetKey(KeyCode.E)) place_rotation += 2f;

        // Press R to start placing an object
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!tempObstacle)
            {
                tempObstacle = Instantiate(obstacle_prefab, hit.point, Quaternion.Euler(0, place_rotation, 0));
                tempObstacleMaterialBackupColor = tempObstacle.GetComponent<Renderer>().material.color;
            }
        }
        
        // Press the left mouse button to release the GameObject currently holding
        if (Input.GetKeyDown(KeyCode.Mouse0) && tempObstacle != null && canPlace)
        {
            tempObstacle.layer = LayerMask.NameToLayer("Obstacle");
            tempObstacle = null;
        }

        // Ray from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Raycast with the previously constructed ray
        if (Physics.Raycast(ray, out hit, 100))
        {
            // If there is a temp obstacle in the scene rotate it
            if (tempObstacle)
            {
                tempObstacle.transform.position = hit.point;                                // Set the position
                tempObstacle.transform.rotation = Quaternion.Euler(0, place_rotation, 0);   // Set the rotation
            }
        }
	}
}
