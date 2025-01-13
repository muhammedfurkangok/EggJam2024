using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Click,
    Key,
    Error,
    IncorretPassword,
    Slice,
    ZombieDeath
}

[Serializable]
public class GameSound
{
    public SoundType key;
    public AudioClip clip;
    public AudioSource externalAudioSource;
}

public class SoundManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private AudioSource mainAudioSource;

    [SerializeField] private List<GameSound> gameSounds = new();
    [SerializeField] private List<GameSound> randomSounds = new();

    public static SoundManager Instance;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayOneShotSound(SoundType key)
    {
        var gameSound = gameSounds.Find(x => x.key == key);

        if (gameSound.externalAudioSource != null)
        {
            gameSound.externalAudioSource.PlayOneShot(gameSound.clip);
        }

        else
        {
            mainAudioSource.PlayOneShot(gameSound.clip);
        }
    }

    public void PlayOneShotSound(SoundType key, float volume)
    {
        var gameSound = gameSounds.Find(x => x.key == key);

        if (gameSound.externalAudioSource != null)
        {
            if (gameSound.externalAudioSource.isPlaying) return;
            gameSound.externalAudioSource.PlayOneShot(gameSound.clip, volume);
        }

        else
        {
            mainAudioSource.PlayOneShot(gameSound.clip, volume);
        }
    }

    public void PlayRandomSoundInArray(SoundType key)
    {
        var gameSound = randomSounds.Find(x => x.key == key);
        gameSound.externalAudioSource.PlayOneShot(gameSound.clip);
    }

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            PlayRandomSoundInArray(SoundType.Key);
        }
        if(Input.GetMouseButtonDown(0))
        {
            PlayOneShotSound(SoundType.Click);
        }
    }
    
    public void PlayErrorSound()
    {
        PlayOneShotSound(SoundType.Error);
    }
    
    public void PlayIncorrectPasswordSound()
    {
        PlayOneShotSound(SoundType.IncorretPassword);
    }

    public void PlayZombieDeathSound()
    {
        PlayOneShotSound(SoundType.ZombieDeath);
    }
}