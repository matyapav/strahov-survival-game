using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNightController : MonoBehaviour {

	public GameObject followTarget;
	public Vector3 offset;
	private Vector3 nextPosition;
	public Vector3 buildingInfoCameraPos;
	public float buildingInfoCameraRotationX;
	public float cameraRotationX;
	private bool showingBuildingInfo = false;
	private Vector3 backupPosition;

	void OnEnable()
	{
		transform.rotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
	}
	void Update () {
		if (showingBuildingInfo)
        {
            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
            {
				transform.rotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
                showingBuildingInfo = false;
                transform.position = backupPosition;
                MainCanvasManager.Instance.HideBuildingsInfo();
            }
        } else {
			 if (Input.GetKeyDown(KeyCode.I) 
                 && !MainCanvasManager.Instance.PauseMenu.activeInHierarchy
                 && !MainCanvasManager.Instance.BlackMarketMenu.activeInHierarchy)
            {
				transform.rotation = Quaternion.Euler(buildingInfoCameraRotationX, 0f, 0f);
                backupPosition = transform.position;
                showingBuildingInfo = true;
                transform.position = buildingInfoCameraPos;
                MainCanvasManager.Instance.ShowBuildingsInfo();
            }
		}
	}
	

	void LateUpdate() {
		if (!showingBuildingInfo) {
			nextPosition = followTarget.transform.position;
			nextPosition.x += offset.x;
			nextPosition.y += offset.y;
			nextPosition.z += offset.z;
			transform.position = nextPosition;
		}
	} 
}
