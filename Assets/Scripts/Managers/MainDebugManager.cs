using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDebugManager : MonoBehaviourSingleton<MainDebugManager> {

    private bool _DEBUG;
    public bool DEBUG;

    public GameObject[] DebugElements;

    private void Start()
    {
        _DEBUG = DEBUG;
        UpdateDebugGameObjects(DEBUG);
    }

    private void LateUpdate()
    {
        if (_DEBUG != DEBUG)
        {
            _DEBUG = DEBUG;
            UpdateDebugGameObjects(DEBUG);
        }

        if (_DEBUG) {
            if (MainDebugManager.Instance.DEBUG)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    MainCanvasManager.Instance.ShowGameOver();
                }
            }
        }
    }

    private void UpdateDebugGameObjects(bool _new_state) {
        for (int i = 0; i < DebugElements.Length; i++)
            {
                DebugElements[i].SetActive(_new_state); 
            } 
    }
}
