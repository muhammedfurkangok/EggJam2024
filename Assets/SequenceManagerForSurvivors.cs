using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SequenceManagerForSurvivors : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Image errorScreen;
    public PlayerController2D playerController2D;

    public System.Action OnErrorGlitch;

    private void Start()
    {
        canvasGroup.DOFade(0, 2.5f).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });

        glitchCountDown();
    }

    private async void glitchCountDown()
    {
        await UniTask.WaitForSeconds(30);
        GiveErrorGlitch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GiveErrorGlitch();
        }
    }

    public void GiveErrorGlitch()
    {
        OnErrorGlitch?.Invoke();
        SoundManager.Instance.PlayErrorSound();
        errorScreen.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f)
            .OnComplete(() =>
            {   
                playerController2D.isGlitch = true;
            });
    }


    public void GlitchyErrorOkayButton()
    {
        canvasGroup.DOFade(1, 2.5f).OnComplete(() => { SceneManager.LoadScene("SecondDesktopScene"); });
    }
}