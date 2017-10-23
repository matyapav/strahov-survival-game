using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainCanvasManager : MonoBehaviour {

    public GameObject gamePausedPanel;

	#region Singleton Instance definition
	private static bool applicationIsQuitting = false;
	private static object _lock = new object();
	private static MainCanvasManager _instance;
	public static MainCanvasManager Instance
	{
		get
		{
			if (applicationIsQuitting)
			{
				return null;
			}

			lock (_lock)
			{
				if (_instance == null)
				{
					GameObject go = new GameObject("Main Canvas Manager");
					go.AddComponent<MainCanvasManager>();
					DontDestroyOnLoad(go);
				}
			}

			return _instance;
		}
	}
		
	void Awake() {
		_instance = this;
	}
		
	public void OnDestroy() {
		applicationIsQuitting = true;
	}
	#endregion

    public UnityEvent PauseAction;
    public UnityEvent ResumeAction;

    private void OnEnable()
    {
        PauseAction.AddListener(PauseGame);
        ResumeAction.AddListener(ResumeGame);
    }

    private void OnDisable()
    {
        PauseAction.RemoveListener(PauseGame);
        ResumeAction.RemoveListener(ResumeGame);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePausedPanel != null && gamePausedPanel.activeSelf != true) {
                PauseAction.Invoke();
            }
        }
	}

    public void PauseGame() {
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
    }
}
