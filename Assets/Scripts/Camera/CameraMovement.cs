using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour {

    [Header("Camera movement")]
    [Tooltip ("The maximum speed that the camera can be moving.")]
    public float maxMovementSpeed = 20.0f;

	[Tooltip("How fast will the movement of the camera change.")]
    [Range(0, 1)]
	public float movementAcceleration = 0.01f;

    [Tooltip("Number <0, 1> that sets the speed of the camera's movement slowing.")]
    [Range(0, 1)]
    public float movementSlowing = 0.8f;

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
    [Tooltip("Zooming speed")]
    public float zoomingSpeed = 20.0f;

    [Tooltip("Minimum y (height) to which user can zoom")]
    public float minY = 6.0f;

    [Tooltip("Maximum y (height) to which user can zoom.")]
    public float maxY = 12.0f;

    [Tooltip("Maximum y (height) will NOT be used. Instead of it a default scene camera y (height) will be used as a maximum.")]
    public bool useDefaultCameraHeight = true;

	private Vector2 CameraSpeed = Vector2.zero;


    private void Start()
    {
        if(useDefaultCameraHeight)
        {
            maxY = Camera.main.transform.position.y;
        }     
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // Flag on if key pressed
        bool key_pressed = false;

        // Handle input and move camera
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - activeBorderThickness)
        {
            CameraSpeed.y = CameraSpeed.y < 0f ? 0f : CameraSpeed.y + movementAcceleration;
            key_pressed = true;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= activeBorderThickness)
        {
			CameraSpeed.y = CameraSpeed.y > 0f ? 0f : CameraSpeed.y - movementAcceleration;
            key_pressed = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - activeBorderThickness)
        {
            CameraSpeed.x = CameraSpeed.x < 0f ? 0f : CameraSpeed.x + movementAcceleration;
            key_pressed = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= activeBorderThickness)
        {
            CameraSpeed.x = CameraSpeed.x > 0f ? 0f : CameraSpeed.x - movementAcceleration;
            key_pressed = true;
        }

        // If there was no keypress lower the camera speed
        if(!key_pressed) {
            if(CameraSpeed.magnitude < 0.1) {
                CameraSpeed = Vector2.zero;                 // If the speed of camera is small, stop the movement
            } else {
                CameraSpeed = CameraSpeed * (4f / 5f);      // If the camera wasn't accelerated make it slower 
            }
        }

        // Clamp the maximum camera speed 
        CameraSpeed.x = Mathf.Clamp(CameraSpeed.x, -maxMovementSpeed, maxMovementSpeed);
        CameraSpeed.y = Mathf.Clamp(CameraSpeed.y, -maxMovementSpeed, maxMovementSpeed);

        // Apply the delta time smoothing
        Vector2 CameraSpeed_delta = CameraSpeed * Time.deltaTime;

        // Assign the pos
		pos += new Vector3(CameraSpeed_delta.x, 0f, CameraSpeed_delta.y);

        // Zoom camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * zoomingSpeed * Time.deltaTime;

        // Apply limits
        pos.x = Mathf.Clamp(pos.x, limitLeft, limitRight);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, limitBottom, limitTop);

        transform.position = pos;
    }
}
