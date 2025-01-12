using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Febucci.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct CutSceneThing
{
    public string text;
    public Image image;
    public CanvasGroup canvasGroup;
    public AudioClip sound;
}

public class CutSceneManager : MonoBehaviour
{
    public List<CutSceneThing> cutSceneThings;
    public TypewriterByCharacter typewriterByCharacter;
    private int currentCutSceneIndex = 0;
    public AudioSource audioSource;

    public void Start()
    {
        MakeCutSceneAnimation();
    }


    public void MakeCutSceneAnimation()
    {
        if (cutSceneThings.Count > 0)
        {
            DisplayCutSceneThing(cutSceneThings[currentCutSceneIndex]);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextCutSceneThing();
        }
    }

    private void NextCutSceneThing()
    {
        currentCutSceneIndex++;

        if (currentCutSceneIndex < cutSceneThings.Count)
        {
            DisplayCutSceneThing(cutSceneThings[currentCutSceneIndex]);
        }
        else
        {
            SceneManager.LoadScene("MainDesktopScene");
        }
    }

    private void DisplayCutSceneThing(CutSceneThing cutSceneThing)
    {
        typewriterByCharacter.ShowText(cutSceneThing.text);
        cutSceneThing.image?.gameObject.SetActive(true);
        if(audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(cutSceneThing.sound);
    }
}