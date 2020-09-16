using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 5f;
    public GameObject AudioSouce;

    private float slowingtTime = 0f;
    private bool slowing = false;
    private AudioSource audioSource;

    
    private void Start()
    {
        audioSource = AudioSouce.GetComponent<AudioSource>();
    }
    
    public void SlowMotion()
    {
        slowing = true;
        slowingtTime = 0f;

        //slow the timeScale
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        ///slow the music
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 0.5f);
    }
    private void LateUpdate()
    {
        slowingtTime += (1f / slowdownLength) * Time.unscaledDeltaTime;
        if (slowingtTime >= 1.0f)
        {
            slowing = false;
        }

        if (slowing)
        {
            Time.timeScale = slowdownFactor;
        }
        else
        {
            // set time to normal
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            //set music to normal
            audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f);
        }

    }
}