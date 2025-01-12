using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastSceneCatSceneManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public CanvasGroup loadingScreen;

    private void OnTriggerEnter(Collider other)
    {
        SeccesefulFound();
        if (other.CompareTag("Player"))
        {
            loadingScreen.gameObject.SetActive(true);
            questText.color = Color.green;
            loadingScreen.DOFade(1, 3f).OnComplete(() =>
            {
                SceneManager.LoadScene("LastScene");
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