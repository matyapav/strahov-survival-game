using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentTest : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject tempObstacle;
    private RaycastHit hit;

    public GameObject obstacle_prefab;

    private float place_rotation = 0f;

	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () 
    {
        // Rotate the game object when placing
        if (Input.GetKey(KeyCode.Q)) place_rotation -= 2f; 
        if (Input.GetKey(KeyCode.E)) place_rotation += 2f;

        // Press R to start placing an object
        if (Input.GetKeyDown(KeyCode.R)) {
            if (!tempObstacle) {
                tempObstacle = Instantiate(obstacle_prefab, hit.point, Quaternion.Euler(0, place_rotation, 0));
            }
        }

        // Press the left mouse button to release the GameObject currently holding
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
            tempObstacle = null;
		}

        // Ray from the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Raycast with the previously constructed ray
        if (Physics.Raycast(ray, out hit, 100)) {
            // If there is a temp obstacle in the scene rotate it
            if (tempObstacle) {
                tempObstacle.transform.position = hit.point;                                // Set the position
                tempObstacle.transform.rotation = Quaternion.Euler(0, place_rotation, 0);   // Set the rotation
            }

            // Set the target of the agent with a right click
			if (Input.GetKeyDown(KeyCode.Mouse1)) {
                NavMeshPath navMeshPath = new NavMeshPath();    // Create a new navmesh path
                agent.CalculatePath(hit.point, navMeshPath);    // Calculate the path

                // Check if the path exist
                if (navMeshPath.status == NavMeshPathStatus.PathComplete) {
                    Debug.Log("Path complete");
                } else if (navMeshPath.status == NavMeshPathStatus.PathPartial) {
                    Debug.Log("Path partial");
                } else if (navMeshPath.status == NavMeshPathStatus.PathInvalid) {
                    Debug.Log("Path invalid");
                }

                // Assign the path to the agent
                agent.path = navMeshPath;
			}

        }
             
	}
}
