using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour {

    [Header("Camera movement")]
    [Tooltip("The maximum speed that the camera can be moving.")]
    public float maxMovementSpeed = 20.0f;
	[Tooltip("How fast will the movement of the camera change."), Range(0, 1)]
	public float movementAcceleration = 0.01f;
    [Tooltip("Number <0, 1> that sets the speed of the camera's movement slowing."), Range(0, 1)]
    public float movementSlowing = 0.8f;

    [Header("Mouse controls")]
    [Tooltip("Enable the mouse controls")]
    public bool screenControlEnabled = false;
    [Tooltip ("How thick should be border around corners which triggers camera movement with mouse.")]
    public float activeBorderThickness = 20f;
    [Tooltip("Limit of camera movement to the left")]
    public float limitLeft;
    [Tooltip("Limit of camera movement to the right")]
    public float limitRight;
    [Tooltip("Limit of camera movement to the top")]
    public float limitTop;
    [Tooltip("Limit of camera movement to the bottom")]
    public float limitBottom;

    [Header("Camera zooming")]
    [Tooltip("Enable or disable zooming")]
    bool scrollingEnabled = true;
    [Tooltip("Zooming speed")]
    public float zoomingSpeed = 20.0f;
    [Tooltip("Minimum y (height) to which user can zoom")]
    public float minY = 6.0f;
    [Tooltip("Maximum y (height) to which user can zoom.")]
    public float maxY = 12.0f;
    [Tooltip("Maximum y (height) will NOT be used. Instead of it a default scene camera y (height) will be used as a maximum.")]
    public bool useDefaultCameraHeight = true;

    [Header("Camera rotation")]
	[Tooltip("Enable or disable the rotation")]
	public bool rotationEnabled = true;
    [Tooltip("The target of the camera rotation")]
    public Transform rotationTarget;

    // The camera's current speed
    private Vector2 cameraSpeed = Vector2.zero;

    private void Start()
    {
        if(useDefaultCameraHeight)
        {
            maxY = Camera.main.transform.position.y;
        }     
    }

	bool HandleInput()
	{
		// Flag ON if there was an user's input
		bool key_pressed = false;

		// Handle input and move camera
        if (Input.GetKey(KeyCode.W) || (Input.mousePosition.y >= Screen.height - activeBorderThickness) 
            && screenControlEnabled)
		{
			cameraSpeed.y = cameraSpeed.y < 0f ? 0f : cameraSpeed.y + movementAcceleration;
			key_pressed = true;
		}
        if (Input.GetKey(KeyCode.S) || (Input.mousePosition.y <= activeBorderThickness) 
            && screenControlEnabled)
		{
			cameraSpeed.y = cameraSpeed.y > 0f ? 0f : cameraSpeed.y - movementAcceleration;
			key_pressed = true;
		}
        if (Input.GetKey(KeyCode.D) || (Input.mousePosition.x >= Screen.width - activeBorderThickness) 
            && screenControlEnabled)
		{
			cameraSpeed.x = cameraSpeed.x < 0f ? 0f : cameraSpeed.x + movementAcceleration;
			key_pressed = true;
		}
        if (Input.GetKey(KeyCode.A) || (Input.mousePosition.x <= activeBorderThickness) 
            && screenControlEnabled)
		{
			cameraSpeed.x = cameraSpeed.x > 0f ? 0f : cameraSpeed.x - movementAcceleration;
			key_pressed = true;
		}

		return key_pressed;
	}

    void RotateCamera() 
    {
        // Calculate the delta vector between current pos and the target
		Vector3 lookPos = rotationTarget.position - transform.position;
        Vector3 target_rotation = Quaternion.LookRotation(lookPos).eulerAngles;
        Vector3 current_rotation = transform.rotation.eulerAngles;

		// Do not rotate around X axis
		target_rotation.x = current_rotation.x;

        // The camera damping
        const float damping = 10f;

        // Lerp to the target
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(target_rotation), Time.deltaTime * damping);
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // Handle the input
        bool key_pressed = HandleInput();

		// If there was no keypress lower the camera speed
		if (!key_pressed)
		{
			if (cameraSpeed.magnitude < 0.1)
			{
				cameraSpeed = Vector2.zero;                 // If the speed of camera is small, stop the movement
			}
			else
			{
				cameraSpeed = cameraSpeed * (4f / 5f);      // If the camera wasn't accelerated make it slower 
			}
		}

        // Clamp the maximum camera speed 
        cameraSpeed.x = Mathf.Clamp(cameraSpeed.x, -maxMovementSpeed, maxMovementSpeed);
        cameraSpeed.y = Mathf.Clamp(cameraSpeed.y, -maxMovementSpeed, maxMovementSpeed);

        // Assign the pos
		pos += new Vector3(cameraSpeed.x, 0f, cameraSpeed.y);

        // Zoom camera
        if(scrollingEnabled) {
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			pos.y -= scroll * zoomingSpeed * Time.deltaTime;
        }

        // Apply limits
        pos.x = Mathf.Clamp(pos.x, limitLeft, limitRight);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, limitBottom, limitTop);

        const float damping = 5f;

        // Lerp to the target rotation
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * damping);
            
        // Rotate the camera
        if(rotationEnabled) {
            RotateCamera();
        }
    }
}
