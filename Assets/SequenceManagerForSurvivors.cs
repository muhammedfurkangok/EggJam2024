using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SequenceManagerForSurvivors : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Image errorScreen;
    private void Start()
    {
        canvasGroup.DOFade(0, 2.5f).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    public void GiveErrorGlitch()
    {
        Time.timeScale = 0f;
        errorScreen.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).SetLoops(10, LoopType.Yoyo);
    }
}