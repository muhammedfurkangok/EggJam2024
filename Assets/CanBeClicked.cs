using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanBeClicked : MonoBehaviour
{
    private Button button;
    [SerializeField] private Button closeTab;
    [SerializeField] private Button closeTab2;
    [SerializeField] private RectTransform openTab;
    [SerializeField] private Vector2 offsetForClosePosition;
    public bool isTabOpen = false;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        if (closeTab != null)
        {
            closeTab.onClick.AddListener(OnCloseTab);
        }

        if (closeTab2 != null)
        {
            closeTab2.onClick.AddListener(OnCloseTab);
        }
    }

    private void OnClick()
    {
        if (!isTabOpen)
        {
            openTab.gameObject.SetActive(true);
            Vector2 startPos = button.transform.position;
            openTab.position = startPos;
            openTab.localScale = Vector3.zero;
            openTab.DOAnchorPos(Vector2.zero, 0.4f).SetEase(Ease.OutBack);
            openTab.DOScale(Vector3.one, 0.4f).OnComplete(() => isTabOpen = true);
        }
    }

    private void OnCloseTab()
    {
        if (isTabOpen)
        {
            Vector2 endPos = button.transform.position;
            openTab.DOAnchorPos(endPos + offsetForClosePosition, 0.5f).SetEase(Ease.InBack);
            openTab.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                openTab.gameObject.SetActive(false);
                isTabOpen = false;
            });
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
        if (closeTab != null)
        {
            closeTab.onClick.RemoveListener(OnCloseTab);
        }

        if (closeTab2 != null)
        {
            closeTab2.onClick.RemoveListener(OnCloseTab);
        }
    }
}