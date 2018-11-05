using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BGMEnum { nightBGM, normalBGM, beBGM,geBGM, tickBGM, }
public class BGMManager : Singleton<BGMManager>
{
    public AudioClip[] bgms;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBGM(BGMEnum.normalBGM);
    }

    public void PlayBGM(BGMEnum bgmEnum){
        audioSource.clip = bgms[(int)bgmEnum];
        audioSource.Play();
    }

    public void PlayNormal()
    {
        PlayBGM(BGMEnum.normalBGM);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
