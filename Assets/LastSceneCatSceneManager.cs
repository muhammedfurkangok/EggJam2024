using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LastSceneCatSceneManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public CanvasGroup loadingScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questText.color = Color.green;
            loadingScreen.DOFade(1, 3f).OnComplete(() =>
            {
                loadingScreen.gameObject.SetActive(true);
                loadingScreen.interactable = true;
                loadingScreen.blocksRaycasts = true;
            });
           
        }
    }


    public void SeccesefulFound()
    {
        questText.color = Color.green;
    }
}