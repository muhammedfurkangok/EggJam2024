using System;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

public class GrabItemPosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameConstants gameConstants;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private BoxCollider collider;

    [Header("Parameters")]
    [SerializeField] private GrabItemType neededGrabItemType;
    [SerializeField] private GrabItemColor neededGrabItemColor;

    [Header("Info")]
    [SerializeField] private bool isCompleted;
    [SerializeField] private GrabItem currentGrabItem;
    [SerializeField] private Color defaultColor;

    private Tween snapTween;

    public bool IsCompleted() => isCompleted;
    public GrabItem GetCurrentGrabItem() => currentGrabItem;
    public GrabItemType GetNeededGrabItemType() => neededGrabItemType;
    public GrabItemColor GetNeededGrabItemColor() => neededGrabItemColor;

    private void Start()
    {
        defaultColor = meshRenderer.material.color;
        collider.size += Vector3.one * gameConstants.grabItemPositionColliderSizeOffset;
    }

    public void SetCompleted(bool willBeCompleted)
    {
        isCompleted = willBeCompleted;
        meshRenderer.material.DOColor(isCompleted ? gameConstants.transparentColor : defaultColor, gameConstants.snapColorChangeDuration)
            .SetEase(gameConstants.snapColorChangeEase);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isCompleted) return;
        if (!other.CompareTag("GrabItem")) return;

        var grabItem = other.GetComponent<GrabItem>();
        currentGrabItem = grabItem;

        if (grabItem.GetGrabItemType() != neededGrabItemType)
        {
            currentGrabItem = null;
            return;
        }

        PlayColorChangeAnimation(true);
        grabItem.TrySnap(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCompleted) return;
        if (!other.CompareTag("GrabItem")) return;
        if (currentGrabItem == null) return;
        if (other != currentGrabItem.GrabItemPositionOnly_GetCollider()) return;

        PlayColorChangeAnimation(true);
    }

    public void PlayColorChangeAnimation(bool isDefault)
    {
        var color = isDefault ? defaultColor : gameConstants.snapColor;

        snapTween?.Kill();
        snapTween = meshRenderer.material.DOColor(color, gameConstants.snapColorChangeDuration)
            .SetEase(gameConstants.snapColorChangeEase);
    }
}