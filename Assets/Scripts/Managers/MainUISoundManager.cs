using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUISoundManager : MonoBehaviourSingleton<MainUISoundManager> {

    [System.Serializable]
    public class AudioSourceWithKey
    {
        public string key;
        public AudioSource sound;
    }

    public List<AudioSourceWithKey> sounds;

    public void Awake()
    {
        Debug.Log(sounds);
    }

    public void PlaySound(string key, bool forcePlay = false)
    {
        if (sounds != null)
        {
            foreach (AudioSourceWithKey aswk in sounds)
            {
                if (aswk.key == key && (forcePlay || !aswk.sound.isPlaying))
                {
                    aswk.sound.Play();
                    return;
                }
            }
        }
    }

    public void StopSound(string key)
    {
        if(sounds != null) {         
			foreach (AudioSourceWithKey aswk in sounds)
			{
				if (aswk != null && aswk.key == key && aswk.sound.isPlaying)
				{
					aswk.sound.Stop();
					return;
				}
			}
        }
    }

}
