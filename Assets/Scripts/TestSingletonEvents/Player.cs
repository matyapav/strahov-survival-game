using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Singleton Instance definition
    private static bool applicationIsQuitting = false;
    private static object _lock = new object();
    private static Player _instance;
    public static Player Instance
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
                    GameObject go = new GameObject("PlayerSingleton");
                    go.AddComponent<Player>();
                    DontDestroyOnLoad(go);
                }
            }

            return _instance;
        }
    }

	// On Awake set the instance to the local variable
	void Awake()
	{
		_instance = this;
	}

	// When exiting the singleton, set the bool to false so the Instance returns null
	public void OnDestroy()
	{
		applicationIsQuitting = true;
	}
    #endregion

    // The c# style of events
    public delegate void OnEnemyHit(Color newColor);    // Create a delegate function which matches the signature of the target
    public static event OnEnemyHit onEnemyHit;          // Create an EVENT instance of the delegate

    // The Unity style of events
    public MyColorEvent m_MyEvent;                      // Can predefined out of runtime



    // Unity style Events must be Initialised
    void OnEnable()
    {
        if (m_MyEvent == null)
        {
            m_MyEvent = new MyColorEvent();
        }
    }

    // React on the SPACE keypress
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // The normal C# way
            onEnemyHit(new Color(Random.value, Random.value, Random.value));

            // The Unity Way
            m_MyEvent.Invoke(new Color(Random.value, Random.value, Random.value));
        }
    }
		
}