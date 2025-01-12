using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LastSceneCatSceneManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public CanvasGroup loadingScreen;
    public GameObject setactiveObje;
    public Image ui;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questText.color = Color.green;
            loadingScreen.DOFade(1, 3f).OnComplete(() =>
            {
                ui.gameObject.SetActive(true);
                setactiveObje.SetActive(false);
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