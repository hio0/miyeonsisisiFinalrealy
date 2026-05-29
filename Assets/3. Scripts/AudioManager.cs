using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioManager
{
    void AudioVolumeSet(float vol);
}

public class AudioManager : MonoBehaviour, IAudioManager
{
    new public static AudioManager audio;

    [SerializeField]AudioSource sound;

    private void Awake()
    {
        if(audio == null)
        {
            audio = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioVolumeSet(float vol)
    {
        sound.volume= vol;
    }
}
