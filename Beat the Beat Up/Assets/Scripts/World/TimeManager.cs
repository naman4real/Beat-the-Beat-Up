using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 10f;
    public GameObject AudioSouce;
    
    private float slowingtTime = 0f;
    private bool slowing = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = AudioSouce.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (slowing)
        {
            slowingtTime += (1f / slowdownLength) * Time.unscaledDeltaTime;
            if (slowingtTime >= 1.0f)
            {
                /*set time to normal*/
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                slowing = false;
                /*set music to normal*/
                audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f);
            }
        }
    }
    public void SlowMotion()
    {
        /*slow the timeScale*/
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        slowingtTime = 0f;
        slowing = true;

        /*slow the music*/
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 0.5f);
    }
}
