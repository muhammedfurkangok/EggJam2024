using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PCSequencesManager : MonoBehaviour
{
    public Image firstGameLoadingScene;
    public Image sceondGameLoadingScene;
    public bool isLoggedIn = false;

    public GameObject lockedScreen;
    

    public void SetFirstGameFullScreenSize()
    {
        RectTransform rt = firstGameLoadingScene.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        rt.DOScale(new Vector3(1, 1, 1), 1f);
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