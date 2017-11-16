using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks the surrounding area and keeps track of the objects around.
/// </summary>
public class NeighbourObjectTracker : MonoBehaviour {
    
    private SphereCollider sc;
    private float scanDistance;     // Is assigned from the ZombieController


    [Tooltip("The list of objects that are currently tracked")]
    public HashSet<GameObject> trackedObjects;  

    [Tooltip("The TAG that will be tracked")]
    public string targetTag = "Zombie";     

    [Tooltip("The step when expanding during the startup scan")]
    public float scanStep = 0.1f;

    // Init the HashSet
	private void Start () 
    {
        trackedObjects = new HashSet<GameObject>();
	}

    // Initialise the NeighbourTracker with correct range and start scanning
    public void Init(float range) 
    {
        if(trackedObjects != null) { 
            trackedObjects.Clear();
        }
        scanDistance = range;
        StartTracking();
    }
	
    // Initial expanding sweep
    private void StartTracking() 
    {
        // Create the sphereCollider
        sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;

        sc.isTrigger = true;  
        sc.radius = 0;   

        // Scan the surrounding area
        for (float i = 0; i < scanDistance; i += scanStep) {
            sc.radius = i / transform.localScale.x;
        }

        // Set the precise radius and remove scaling
        sc.radius = scanDistance / transform.localScale.x;
    }

    // Add the GameObject if it enters the collider field
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag && other.gameObject.activeSelf) {
            trackedObjects.Add(other.gameObject); 
        }
    }

    // On exiting check if the object is really leaving and if yes, delete it
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag) {
            if (!TurretController.IsInRange(transform, other.transform, scanDistance)) {
                if (trackedObjects.Contains(other.gameObject)) {
                    trackedObjects.Remove(other.gameObject);
                }
            }
        }
    }

    // Force check for objects that are not valid
    public void ForceCheck() 
    {
        List<GameObject> toDelete = new List<GameObject>();

        foreach (GameObject g in trackedObjects) {
            if (TurretController.IsInRange(transform, g.transform, scanDistance)) {
                toDelete.Add(g);
            }
        }

        // Delete the bad ones
        for (int i = 0; i < toDelete.Count; i++) {
            trackedObjects.Remove(toDelete[i]);
        }
    }
}
