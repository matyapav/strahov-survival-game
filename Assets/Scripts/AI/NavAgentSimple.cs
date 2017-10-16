using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentSimple : MonoBehaviour {

    private NavMeshAgent agent;

	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast with the previously constructed ray
        if (Physics.Raycast(ray, out hit, 100)) {
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
