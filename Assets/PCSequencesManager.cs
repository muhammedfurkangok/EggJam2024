using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PCSequencesManager : MonoBehaviour
{
    public Image firstGameLoadingScene;
    public Image firstGameLoadingScene2;
    public Image sceondGameLoadingScene;
    public bool isLoggedIn = false;

    public GameObject lockedScreen;


    public async void SetFirstGameFullScreenSize()
    {
        RectTransform rt2 = firstGameLoadingScene2.rectTransform;
        rt2.DOSizeDelta(new Vector2(4000, 3000), 0.5f).SetEase(Ease.OutQuad);

        foreach (RectTransform child in rt2)
        {
            child.DOSizeDelta(new Vector2(child.rect.width + 100, child.rect.height + 100), 0.5f).SetEase(Ease.OutQuad);
        }

        await UniTask.WaitForSeconds(2f);
        SceneManager.LoadScene("SurvivorsScene");
    }

    public async void SetsSecondGameFullScreenSize()
    {
        RectTransform rt2 = sceondGameLoadingScene.rectTransform;
        rt2.DOSizeDelta(new Vector2(4000, 3000), 0.5f).SetEase(Ease.OutQuad);

        foreach (RectTransform child in rt2)
        {
            child.DOSizeDelta(new Vector2(child.rect.width + 100, child.rect.height + 100), 0.5f).SetEase(Ease.OutQuad);
        }

        await UniTask.WaitForSeconds(0.5f);
        SceneManager.LoadScene("PuzzleScene");
    }
}