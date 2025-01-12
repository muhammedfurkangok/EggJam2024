using System;
using DG.Tweening;
using UnityEngine;

public class SequenceManagerForDesktopScene : MonoBehaviour
{
  public CanvasGroup canvasGroup;

  private void Start()
  {
    canvasGroup.DOFade( 0, 2.5f ).OnComplete( () =>
    {
      canvasGroup.interactable = false;
      canvasGroup.blocksRaycasts = false;
    } );
  }
}
