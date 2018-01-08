using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstaclePlacer : MonoBehaviourSingleton<ObstaclePlacer> {

    private GameObject tempObstacle;
    //private Color tempObstacleMaterialBackupColor;
    private RaycastHit hit;
    private float place_rotation = 0f;
    private bool canPlace = true;

    private GameObject obstacle_prefab;
    private Vector3 obstacleRotation;
    private float obstaclePrice;

    private bool canBeDeletedDuringPlacing = true;

    public void SetObstacle(GameObject obstacle, float price, bool alreadyInScene = false)
    {
        obstacleRotation = obstacle.transform.rotation.eulerAngles;
        // Debug.Log(obstacleRotation);
        obstacle_prefab = obstacle;
        obstaclePrice = price;
        canBeDeletedDuringPlacing = true;
        if(alreadyInScene)
        {
            tempObstacle = obstacle;
            obstaclePrice = 0;
            tempObstacle.layer = LayerMask.NameToLayer("Default");
            canBeDeletedDuringPlacing = false;
        }
    }

	
	// Update is called once per frame
	void Update () {
        if(obstacle_prefab != null) { 
            //Handle creating obstacle in scene
            if (!tempObstacle)
            {
                // Do not rotate the turret otherwise it will get screwed
                if (obstacle_prefab.tag == "Turret") {
                    tempObstacle = Instantiate(obstacle_prefab, hit.point, Quaternion.Euler(obstacleRotation.x, 0, obstacleRotation.z));
                }
                else {
                    tempObstacle = Instantiate(obstacle_prefab, hit.point, Quaternion.Euler(obstacleRotation.x, place_rotation, obstacleRotation.z));
                }

                //tempObstacleMaterialBackupColor = tempObstacle.GetComponent<Renderer>().material.color;
                    
            }
                
            //Handle moving obstacle in scene
            if (tempObstacle)
            {
                //TODO opravit .. zatim nechame placovat vsude
               /*Collider[] overlapedObjects = Physics.OverlapBox(tempObstacle.transform.position, tempObstacle.transform.localScale /2, tempObstacle.transform.rotation, LayerMask.GetMask("Obstacle"));
                if (overlapedObjects.Length > 0)
                {
                    //hits another obstacle 
                    tempObstacle.GetComponent<Renderer>().material.color = Color.red; //TODO resit zacervenani nejakym shaderem
                    canPlace = false;
                }
                else
                {
                    canPlace = true;
                    tempObstacle.GetComponent<Renderer>().material.color = tempObstacleMaterialBackupColor; 
                }*/

                if (tempObstacle.GetComponent<PlacingEvents>()){
                    tempObstacle.GetComponent<PlacingEvents>().InvokeOnBeginPlacing();
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    tempObstacle.transform.position = new Vector3(hit.point.x, 0f ,hit.point.z);
                    // Set the rotation if the obstacle is not Turret
                    if (tempObstacle.tag != "Turret") {
                        tempObstacle.transform.rotation = Quaternion.Euler(obstacleRotation.x, place_rotation, obstacleRotation.z);
                    }
                }

                // Update rotation
                if(tempObstacle.tag != "Turret") {
                    if (Input.GetKey(KeyCode.Q)) place_rotation -= 2f;
                    if (Input.GetKey(KeyCode.E)) place_rotation += 2f;
                }

                //Handle placing
                if (Input.GetKeyDown(KeyCode.Mouse1) && tempObstacle != null && canBeDeletedDuringPlacing)
                {
                    Destroy(tempObstacle);
                    tempObstacle = null;
                    obstacle_prefab = null;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0) && tempObstacle != null && canPlace)
                {
                    if (CurrencyController.Instance.DescreaseValue(obstaclePrice)) { 
                        tempObstacle.layer = LayerMask.NameToLayer("Obstacle");
                        if(tempObstacle.GetComponent<PlacingEvents>()){
                            tempObstacle.GetComponent<PlacingEvents>().InvokeOnPlaced();
                        }
                        tempObstacle = null;
                        obstacle_prefab = null;
                       
                    } else
                    {
                        MessageController.Instance.AddMessage("You cannot afford this!", 3f, Color.red);
                    }
                    
                }
            }
        }
    }
}
