using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour {

    [Header("Camera movement")]
    [Tooltip ("How fast will the camera movement be.")]
    public float movementSpeed = 20.0f;
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

        //handle input and move camera
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - activeBorderThickness)
        {
            pos.z += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= activeBorderThickness)
        {
            pos.z -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - activeBorderThickness)
        {
            pos.x += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= activeBorderThickness)
        {
            pos.x -= movementSpeed * Time.deltaTime;
        }

        //zoom camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * zoomingSpeed * Time.deltaTime;

        //apply limits
        pos.x = Mathf.Clamp(pos.x, -limitLeft, limitRight);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -limitBottom, limitTop);

        transform.position = pos;
    }
}
