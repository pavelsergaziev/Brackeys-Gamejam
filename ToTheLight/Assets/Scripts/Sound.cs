using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound  {

    public AudioClip clip;
    public string name;
    [Range(0f,1f)]
    public float volume;
    public float pitch;
    public bool loop;
    public bool playOnAwake;
    public AudioMixerGroup audioMixer;
    [HideInInspector]
    public AudioSource source;

}
