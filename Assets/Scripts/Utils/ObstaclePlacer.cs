using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstaclePlacer : MonoBehaviourSingleton<ObstaclePlacer> {

    private GameObject tempObstacle;
    private Color tempObstacleMaterialBackupColor;
    private RaycastHit hit;
    private float place_rotation = 0f;
    private bool canPlace = true;

    private GameObject obstacle_prefab;
    private Vector3 obstacleRotation;
    private float obstaclePrice;

    public void SetObstacle(GameObject obstacle, float price, bool alreadyInScene = false)
    {
        obstacleRotation = obstacle.transform.rotation.eulerAngles;
        // Debug.Log(obstacleRotation);
        obstacle_prefab = obstacle;
        obstaclePrice = price;
        if(alreadyInScene)
        {
            tempObstacle = obstacle;
            obstaclePrice = 0;
            tempObstacle.layer = LayerMask.NameToLayer("Default");
        }
    }

	
	// Update is called once per frame
	void Update () {
        if(obstacle_prefab != null) { 
            //Handle creating obstacle in scene
            if (!tempObstacle)
            {
                tempObstacle = Instantiate(obstacle_prefab, hit.point, Quaternion.Euler(obstacleRotation.x, place_rotation, obstacleRotation.z));
                tempObstacleMaterialBackupColor = tempObstacle.GetComponent<Renderer>().material.color;
                    
            }
                
            //Handle moving obstacle in scene
            if (tempObstacle)
            {
                if (Physics.OverlapBox(tempObstacle.transform.position, tempObstacle.transform.localScale / 2, tempObstacle.transform.rotation, LayerMask.GetMask("Obstacle")).Length > 0)
                {
                    //hits another obstacle 
                    tempObstacle.GetComponent<Renderer>().material.color = Color.red; //TODO resit zacervenani nejakym shaderem
                    canPlace = false;
                }
                else
                {
                    canPlace = true;
                    tempObstacle.GetComponent<Renderer>().material.color = tempObstacleMaterialBackupColor; 
                }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    tempObstacle.transform.position = new Vector3(hit.point.x, tempObstacle.transform.localScale.y / 2 ,hit.point.z);
                    tempObstacle.transform.rotation = Quaternion.Euler(obstacleRotation.x, place_rotation, obstacleRotation.z);   // Set the rotation
                }
                //Handle placing
                if (Input.GetKey(KeyCode.Q)) place_rotation -= 2f;
                if (Input.GetKey(KeyCode.E)) place_rotation += 2f;
                if (Input.GetKeyDown(KeyCode.Mouse1) && tempObstacle != null)
                {
                    Destroy(tempObstacle);
                    tempObstacle = null;
                    obstacle_prefab = null;
                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && tempObstacle != null && canPlace)
                {
                    if (CurrencyController.Instance.DescreaseValue(obstaclePrice)) { 
                        tempObstacle.layer = LayerMask.NameToLayer("Obstacle");
                        if (tempObstacle.GetComponent<MultipleChoiceTrigger>())
                        {
                            tempObstacle.GetComponent<MultipleChoiceTrigger>().Activate();
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
