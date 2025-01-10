using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BabyLaugh,
    Explosion,
    Fov,
    Intel,
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
    [Header("References")]
    [SerializeField] private AudioSource mainAudioSource;
    [SerializeField] private List<GameSound> gameSounds = new();

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
}