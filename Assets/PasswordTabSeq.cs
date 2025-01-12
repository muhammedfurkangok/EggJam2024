using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PasswordTabSeq : MonoBehaviour
{
    public bool isCanBeLogin = false;
    public Image directoryTab;

    public void SetLoginState()
    {
        isCanBeLogin = true;
    }

    public void OpenLockedScreen()
    {
        if (isCanBeLogin)
        {
            directoryTab.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
        else
        {
            Debug.Log("Login Failed");
        }
    }
}