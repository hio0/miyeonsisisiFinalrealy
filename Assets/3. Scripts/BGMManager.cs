using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour, IAudioManager
{
    public static BGMManager BGM;
    [SerializeField] AudioSource bgm;

    private void Awake()
    {
        if (BGM == null)
        {
            BGM = this;
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
        bgm.volume= vol;
    }
}
