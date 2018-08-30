using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour {

    public Sound[] sounds;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;
            s.source.playOnAwake = s.playOnAwake;
            
        }
    }
    
    private void Start()
    {
        PlaySound("CatarpillarTheme");
        PlaySound("ForestAmbience");
    }

    public void PlaySound(string soundName,bool playOnce)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (!playOnce)
        {
            s.source.Play();
        }
        else
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();  
            }
        }
    }
    public void PlaySound(string soundName)
    {

        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        
        s.source.Play();

    }
    public void StopPlaySound(string soundName)
    {

        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            throw new Exception("отсутствует ссылка на звуковой файл " + soundName);
        }
        s.source.Stop();

    }
    public bool Isplaying(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            throw new Exception("отсутствует ссылка на звуковой файл " + soundName);
        }
        return s.source.isPlaying;
        
    }
    public IEnumerator FadeIn(string soundName, float fadeTime)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        float t = 0f;
        s.source.volume = 0f;
        while (t<=fadeTime)
        {
            s.source.volume = Mathf.Lerp(0f, 1f, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        s.source.volume = 1f;
        
    }
    public IEnumerator FadeOut(string soundName, float fadeTime, bool stopAfterFade)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        float t = 0f;
        float x = s.source.volume;
        while (t<=fadeTime)
        {
            s.source.volume = Mathf.Lerp(x, 0f, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        s.source.volume = 0f;
        if (stopAfterFade)
        {
            StopPlaySound(s.name);
        }
    }
    public void CrossFade(string fadeInTrackName, string fadeOutTrackName, float fadeInTime, float foadeOutTime, bool stopSecondTrack)
    {
        StartCoroutine(FadeIn(fadeInTrackName, fadeInTime));
        PlaySound(fadeInTrackName);
        
        StartCoroutine(FadeOut(fadeOutTrackName, fadeInTime, stopSecondTrack));
        
    }
    public IEnumerator PlayAfter(string playingTrackName, string nextTrackName, float timeBeforeEndOfPlayingTrack, float fadeInTime, float fadeOutTime)
    {
        Sound playingTrack = Array.Find(sounds, sound => sound.name == playingTrackName);
        float time = playingTrack.clip.length - playingTrack.source.time - timeBeforeEndOfPlayingTrack;
        if (time<0)
        {
            time = 0;
        }
        yield return new WaitForSeconds(time);
        CrossFade(nextTrackName, playingTrackName, fadeInTime, fadeOutTime, true);
    }
    public void Transformation()
    {
        PlaySound("TransformationStart");
        CrossFade("TransformationTheme","CatarpillarTheme",3f, 3f, true);
        StartCoroutine(PlayAfter("TransformationTheme","ButterflyMusTheme_1", 6.857f, 0.1f, 6f));
    }
    public void PlaySecondPartOfmainTheme()
    {
        Debug.Log("x");
        StartCoroutine(PlayAfter("ButterflyMusTheme_1", "ButterflyMusTheme_2", 0.1f, 0.1f,0.1f));
        
    }
    public void SoundPitch(string soundName, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (pitch >3)
        {
            pitch = 3;
        }
        if (pitch <-3)
        {
            pitch = -3;
        }
        s.source.pitch = pitch;
    }
    public Sound GetSound(string soundname)
    {
        return Array.Find(sounds, sound => sound.name == soundname);
    }



}
