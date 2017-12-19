using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDayController : MonoBehaviour 
{
    public Vector3 defaultCameraPosition;
    public Vector3 defaultCameraRotation;
    [Header("Camera movement")]
    [Tooltip("The maximum speed that the camera can be moving.")]
    public float maxMovementSpeed = 20.0f;
	[Tooltip("How fast will the movement of the camera change."), Range(0, 2)]
	public float movementAcceleration = 0.01f;
    [Tooltip("Number <0, 1> that sets the speed of the camera's movement slowing."), Range(0, 1)]
    public float movementSlowing = 0.8f;

    [Header("Mouse controls")]
    [Tooltip("Enable the mouse controls")]
    public bool screenControlEnabled = false;
    [Tooltip ("How thick should be border around left and right corners in percentage which triggers camera movement with mouse.")]
    public float activeBorderThicknessX = 20f;
    [Tooltip("How thick should be border around top and bottom corners in percentage corners which triggers camera movement with mouse.")]
    public float activeBorderThicknessY = 20f;
    [Tooltip("Limit of camera movement to the left")]
    public float limitLeft;
    [Tooltip("Limit of camera movement to the right")]
    public float limitRight;
    [Tooltip("Limit of camera movement to the top")]
    public float limitTop;
    [Tooltip("Limit of camera movement to the bottom")]
    public float limitBottom;

    [Header("Keyboard controls")]
    [Tooltip("Enable the keyboard controls")]
    public bool keyboardControlsEnabled = true;

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
    private Vector2 cameraSpeed = Vector2.zero;
    private void Start()
    {
        if (useDefaultCameraHeight)
        {
            maxY = Camera.main.transform.position.y;
        }
        activeBorderThicknessY = Screen.height / 100 * activeBorderThicknessY;
        activeBorderThicknessX = Screen.width / 100 * activeBorderThicknessX;
    }

    private void OnEnable()
    {
        transform.position = defaultCameraPosition;  
        transform.eulerAngles = defaultCameraRotation; 
    }

	Vector2 GetInput()
	{
        Vector2 input_out = Vector2.zero;

        // If the screen handling with mouse is enabled
        if (screenControlEnabled) {
			if (Input.mousePosition.y >= Screen.height - activeBorderThicknessY) {
				input_out.y += 1;
			}
            if (Input.mousePosition.y <= activeBorderThicknessY) {
				input_out.y += -1;
			}
            if (Input.mousePosition.x >= Screen.width - activeBorderThicknessX) {
				input_out.x += 1;
			}
			if (Input.mousePosition.x <= activeBorderThicknessX) {
				input_out.x += -1;
			}
        }

        // If the keyboard input is enabled
        if (keyboardControlsEnabled) {
            if (Input.GetKey(KeyCode.W)) {
                input_out.y += 1;
			}
            if (Input.GetKey(KeyCode.S)) {
				input_out.y -= 1;
			}
            if (Input.GetKey(KeyCode.D)) {
				input_out.x += 1;
			}
            if (Input.GetKey(KeyCode.A)) {
				input_out.x -= 1;
			}
        }

        // Clamp the output to <-1, 1>
        input_out.x = Mathf.Clamp(input_out.x, -1, 1);
        input_out.y = Mathf.Clamp(input_out.y, -1, 1);
        
        return input_out;
	}

    void RotateCamera() 
    {
        // Calculate the delta vector between current pos and the target
		Vector3 lookPos = rotationTarget.position - transform.position;
        Vector3 target_rotation = Quaternion.LookRotation(lookPos).eulerAngles;
        Vector3 current_rotation = transform.rotation.eulerAngles;

		// Do not rotate around X axis
		// target_rotation.x = current_rotation.x;

        // The camera damping
        const float damping = 10f;

        // Lerp to the target
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(target_rotation), Time.deltaTime * damping);
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // Handle the input
        Vector2 input = GetInput();

        // Make a fast snappy break
        if ((cameraSpeed.y < 0 && input.y > 0) || (cameraSpeed.y > 0 && input.y < 0)) {
            cameraSpeed.y = 0;
        }
        if ((cameraSpeed.x < 0 && input.x > 0) || (cameraSpeed.x > 0 && input.x < 0)) {
            cameraSpeed.x = 0;
        }

        // Add the input scaled by movement acceleration
        cameraSpeed += input * movementAcceleration;


        // If there was no keypress lower the camera speed
        if (input == Vector2.zero) {
            if (cameraSpeed.magnitude < 0.1) {
                cameraSpeed = Vector2.zero;                 // If the speed of camera is small, stop the movement
            } else {
                cameraSpeed = cameraSpeed * (4f / 5f);      // If the camera wasn't accelerated make it slower 
            }
        }

        // Clamp the maximum camera speed 
        cameraSpeed = Vector2.ClampMagnitude(cameraSpeed, maxMovementSpeed);

        // Assign the pos
        pos += new Vector3(cameraSpeed.x, 0f, cameraSpeed.y);

        // Zoom camera if enabled
        if (scrollingEnabled)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            //pos.y -= scroll * zoomingSpeed * Time.deltaTime;
            Vector3 deltaPos = transform.forward * scroll * zoomingSpeed;
            if(pos.y + deltaPos.y >= minY && pos.y + deltaPos.y <= maxY) {
                pos += deltaPos;
            }
        }

        // Rotate the camera if enabled
        if (rotationEnabled)
        {
            RotateCamera();
        }

        // Apply limits
        pos.x = Mathf.Clamp(pos.x, limitLeft, limitRight);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, limitBottom, limitTop);

        const float damping = 5f;

        // Lerp to the target rotation
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * damping);
    }
}
