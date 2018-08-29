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
    IEnumerator FadeIn(Sound sound, float fadeTime)
    {
        float t = 0f;
        sound.source.volume = 0f;
        while (t<fadeTime)
        {
            sound.source.volume = Mathf.Lerp(0f, 1f, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        sound.source.volume = 1f;
        
    }
    IEnumerator FadeOut(Sound sound, float fadeTime, bool stopAfterFade)
    {
        float t = 0f;

        while (t<fadeTime)
        {
            sound.source.volume = Mathf.Lerp(sound.source.volume, 0f, t / fadeTime);
            t += Time.deltaTime;
            yield return null;
        }
        sound.source.volume = 0f;
        if (stopAfterFade)
        {
            StopPlaySound(sound.name);
        }
    }
    public void CrossFade(Sound sound1, Sound sound2, float fadeInTime, float foadeOutTime, bool stopSecondTrack)
    {
        StartCoroutine(FadeIn(sound1, fadeInTime));
        sound1.source.Play();
        StartCoroutine(FadeOut(sound2, fadeInTime, stopSecondTrack));
        
    }
    IEnumerator PlayAfter(Sound sound1,Sound sound2)
    {
        
        yield return new WaitForSeconds(sound1.clip.length-0.1f);
        CrossFade(sound2, sound1, 0.05f, 0.1f, true);
    }
    public void Transformation()
    {
        CrossFade(Array.Find(sounds, sound => sound.name == "TransformationTheme"),
                      Array.Find(sounds, sound => sound.name == "CatarpillarTheme"),
                      .1f, 2f, true);
        StartCoroutine(PlayAfter(Array.Find(sounds, sound => sound.name == "TransformationTheme"),
                  Array.Find(sounds, sound => sound.name == "ButterflyMusTheme_1")));
    }


}
