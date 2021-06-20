using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public Sound[] sounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string soundName)
    {
        try
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            s.source.Play();
        }
        catch (Exception)
        {
            throw new Exception("Sound '" + soundName + "'" + " not find in the Sound Controller Array. Please asssign It to sounds list Array.");
        }
    }

    public void PlayRandomSwitchSound()
    {
        PlayRandomVariation($"Switch{UnityEngine.Random.Range(1, 8)}");
    }

    public void PlayButtonClick()
    {
        Play("Button");
    }

    public void PlayRandomVariation(string soundName)
    {
        try
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            float oldPitch = s.source.pitch;
            float oldVolume = s.source.volume;
            s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            s.source.volume = UnityEngine.Random.Range(0.4f, 0.6f);
            s.source.Play();
            s.pitch = oldPitch;
            s.volume = oldVolume;
        }
        catch (Exception)
        {
            throw new Exception("Sound '" + soundName + "'" + " not find in the Sound Controller Array. Please asssign It to sounds list Array.");
        }
    }

    public void Stop(string soundName)
    {
        try
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            s.source.Stop();
        }
        catch (Exception)
        {
            throw new Exception("Sound '" + soundName + "'" + " not find in the Sound Controller Array. Please asssign It to sounds list Array.");
        }
    }
}
