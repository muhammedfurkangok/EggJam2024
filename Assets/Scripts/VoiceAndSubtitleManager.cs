using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;

public enum VoiceType
{
    Hospital1,
    Eye1,
    Eye2,
    ChildRoom1,
    ChildRoom2,
    ChildRoom3,
    ChildRoom4,
    DoorRoomEnter,
    DoorRoomCross,
    DoorRoomMusic,
    DoorRoomBone,
    LastRoom,
    LessEye,
}

[Serializable]
public class VoiceEntry
{
    public VoiceType voiceType;
    public string text;
    public AudioClip audioClip;
    public float displayDuration;
}

public class VoiceAndSubtitleManager : MonoBehaviour
{
    [Header("Subtitles")]
    [SerializeField] private List<VoiceEntry> subtitles;

    [Header("References")]
    [SerializeField] private TypewriterByCharacter typewriter;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private AudioSource audioSource;

    private Coroutine subtitleCoroutine;

    public static VoiceAndSubtitleManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (typewriter != null && !typewriter.isShowingText) typewriter.ShowText(" ");

        if (subtitleText == null || audioSource == null)
        {
            Debug.LogError("Subtitle Text or AudioSource is not assigned.");
        }
    }

    public void Play(VoiceType voiceType)
    {
        var index = subtitles.FindIndex(x => x.voiceType == voiceType);
        if (index == -1)
        {
            Debug.LogError("Subtitle type not found.");
            return;
        }

        subtitleCoroutine = StartCoroutine(DisplaySubtitle(subtitles[index]));
    }

    private IEnumerator DisplaySubtitle(VoiceEntry entry)
    {
        typewriter.ShowText(entry.text);
        
        if (entry.audioClip != null)
        {
            audioSource.clip = entry.audioClip;
            audioSource.Play();
        }


        var duration = Mathf.Max(entry.displayDuration, entry.audioClip != null ? entry.audioClip.length : 0);
        yield return new WaitForSeconds(duration);

        subtitleText.text = "";
    }

    public void StopSubtitle()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
            subtitleCoroutine = null;
        }

        audioSource.Stop();
        subtitleText.text = "";
    }
}