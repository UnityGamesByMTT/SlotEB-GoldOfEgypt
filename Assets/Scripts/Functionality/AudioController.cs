using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource bg_adudio;
    [SerializeField] internal AudioSource audioPlayer_wl;
    [SerializeField] internal AudioSource audioPlayer_button;
    [SerializeField] private AudioClip[] clips;


    private void Start()
    {
        audioPlayer_button.clip = clips[clips.Length-1];
    }

    internal void PlayWLAudio(string type)
    {
        
        int index = 0;
        switch (type)
        {
            case "spin":
                index = 0;
                break;
            case "win":
                index = UnityEngine.Random.Range(1, 2);
                break;
            case "lose":
                index = 3;
                break;
        }
        StopWLAaudio();
        audioPlayer_wl.clip = clips[index];
        audioPlayer_wl.loop = true;
        audioPlayer_wl.Play();

    }

    internal void PlayButtonAudio() {
        StopButtonAudio();
        audioPlayer_button.Play();
        Invoke("StopButtonAudio", audioPlayer_button.clip.length);

    }

    internal void StopWLAaudio()
    {
        audioPlayer_wl.Stop();
        audioPlayer_wl.loop = false;
    }

    internal void StopButtonAudio() {

        audioPlayer_button.Stop();

    }

    internal void StopBgAudio() {
        bg_adudio.Stop();

    }

    internal void ToggleMute(bool toggle, string type="all") {

        switch (type)
        {
            case "bg":
                bg_adudio.mute = toggle;
                break;
            case "button":
                audioPlayer_button.mute=toggle;
                break;
            case "wl":
                audioPlayer_wl.mute=toggle;
                break;
            case "all":
                audioPlayer_wl.mute = toggle;
                bg_adudio.mute = toggle;
                audioPlayer_button.mute = toggle;
                break;
        }
    }

}
