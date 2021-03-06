﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SFXEnum
{
    findBug,
    failFindBug,
    hitOnWall,
    getPoint,
    buttonClick,
};

public class SFXManager : Singleton<SFXManager>
{
    AudioSource audioSource;

   
    public AudioClip[] clips;

    // Use this for initialization
    void Start()
    {
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
        //ChangeVolume(PlayerPrefs.GetFloat(CSConstant.SFXVolumePref, 1));
    }

    public void ButtonClick()
    {
        audioSource.clip = clips[(int)SFXEnum.buttonClick];
        audioSource.Play();
    }


    public void PlaySFX(SFXEnum sfxEnum){
        if (audioSource)
        {
            audioSource.clip = clips[(int)sfxEnum];
            audioSource.Play();
        }
    }

    public AudioClip SfxClip(SFXEnum sfxEnum)
    {
        return clips[(int)sfxEnum];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
