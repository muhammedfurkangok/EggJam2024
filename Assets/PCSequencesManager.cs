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


    public void SetFirstGameFullScreenSize()
    {
        RectTransform rt2 = firstGameLoadingScene2.rectTransform;
        rt2.DOSizeDelta(new Vector2(2000, 1100), 0.5f).SetEase(Ease.OutQuad);

        foreach (RectTransform child in rt2)
        {
            
            child.DOSizeDelta(new Vector2(child.rect.width + 100, child.rect.height + 100), 0.5f).SetEase(Ease.OutQuad);
        }
    }

    public void SetsSecondGameFullScreenSize()
    {
        RectTransform rt = firstGameLoadingScene.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        rt.DOScale(new Vector3(1, 1, 1), 1f);
    }

    public async void EnterTheFirstGame()
    {
        await UniTask.WaitForSeconds(2f);
        SceneManager.LoadScene("SurvivorsScene");
    }

    public async void EnterTheSecondGame()
    {
        await UniTask.WaitForSeconds(2f);

        SceneManager.LoadScene("PuzzleScene");
    }
}