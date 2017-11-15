using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks the surrounding area and keeps track of the objects around.
/// </summary>
public class NeighbourObjectTracker : MonoBehaviour {

    // Sphere collider trigger
    SphereCollider sc;

    // The list of objects that are currently tracked
    public HashSet<GameObject> trackedObjects;

    // The TAG that will be tracked
    public string targetTag = "Zombie";

    [Tooltip("Area around the object that will be scanned")]
    private float scanDistance = 10;    // Is assigned from the ZombieController

    [Tooltip("The step when expanding during the startup scan")]
    public float scanStep = 0.1f;

    // Init the HashSet
	void Start () {
        trackedObjects = new HashSet<GameObject>();
	}

    // Initialise the NeighbourTracker with correct range and start scanning
    public void Init(float range) 
    {
        trackedObjects.Clear(); // Clear the array to make sure
        scanDistance = range;   // Set the range
        StartTracking();        // Do the first sweep
    }
	
    private void StartTracking() 
    {
        // Create the sphereCollider
        sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;

        // Set it as trigger
        sc.isTrigger = true;

        // Set the radius to 0
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
        // On trigger check the other gameObject and scan it if the tag matches
        if (other.tag == targetTag && other.gameObject.activeSelf) {
            trackedObjects.Add(other.gameObject); 
            // Debug.Log("Adding " + other.name);
        }
    }

    // On exiting check if the object is really leaving and if yes, delete it
    private void OnTriggerExit(Collider other)
    {
        // On trigger check the other gameObject and scan it if the tag matches
        if (other.tag == targetTag)
        {
            if(!TurretController.IsInRange(transform, other.transform, scanDistance)) {
                if (trackedObjects.Contains(other.gameObject))
                {
                    trackedObjects.Remove(other.gameObject);
                }
            }
        }
    }

    // Force check for objects that are not valid
    public void ForceCheck() {
        // List of objects that will be deleted
        List<GameObject> toDelete = new List<GameObject>();

        // Check if the objecs that I'm tracking are valid
        foreach (GameObject g in trackedObjects)
        {
            if (TurretController.IsInRange(transform, g.transform, scanDistance))
            {
                toDelete.Add(g);
            }
        }

        // Delete the bad ones
        for (int i = 0; i < toDelete.Count; i++)
        {
            trackedObjects.Remove(toDelete[i]);
        }
    }
}
