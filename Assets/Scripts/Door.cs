using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameConstants gameConstants;
    [SerializeField] private GrabItem[] grabItems;
    [SerializeField] private Rigidbody[] grabItemRigidbodies;
    [SerializeField] private Transform rotateDoor;

    [Header("Parameters")]
    [SerializeField] private bool isGrabItemsEnabledOnStart;
    [SerializeField] private bool willoadSceneOnTrigger;
    [SerializeField] private int sceneIndexToLoad;

    [Header("Info")]
    [SerializeField] private bool isOpen;

    private Tween rotateTween;

    private void Start()
    {
        if (!isGrabItemsEnabledOnStart)
        {
            foreach (var grabItem in grabItems) grabItem.enabled = false;
            foreach (var rigidbody in grabItemRigidbodies) rigidbody.isKinematic = true;
        }
    }

    public void ToggleDoor()
    {
        OpenDoor().Forget();
    }

    private async UniTask OpenDoor()
    {
        if (isOpen) return;
        isOpen = true;

        var rotateDoorParentParent = rotateDoor.parent.parent;
        var rotateDoorParent = rotateDoor.parent;
        rotateDoor.SetParent(rotateDoor.parent.parent);
        rotateDoorParent.SetParent(rotateDoor);

        await rotateDoor.DOLocalRotate(gameConstants.doorOpenRotation, gameConstants.doorOpenDuration)
            .SetEase(gameConstants.doorOpenEase);

        rotateDoor.SetParent(rotateDoorParent);
        rotateDoorParent.SetParent(rotateDoorParentParent);
    }

    private async UniTask CloseDoor()
    {
        isOpen = false;

        var rotateDoorParentParent = rotateDoor.parent.parent;
        var rotateDoorParent = rotateDoor.parent;
        rotateDoor.SetParent(rotateDoor.parent.parent);
        rotateDoorParent.SetParent(rotateDoor);

        await rotateDoor.DOLocalRotate(gameConstants.doorCloseRotation, gameConstants.doorCloseDuration)
            .SetEase(gameConstants.doorCloseEase);

        rotateDoor.SetParent(rotateDoorParent);
        rotateDoorParent.SetParent(rotateDoorParentParent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && willoadSceneOnTrigger)
        {
            PlayerPrefs.SetInt("first_scene", sceneIndexToLoad);

            var completedScenes = PlayerPrefs.GetInt("completed_scenes", 0);
            completedScenes++;
            PlayerPrefs.SetInt("completed_scenes", completedScenes);

            if (completedScenes == 2) sceneIndexToLoad = 6;

            SceneManager.LoadScene(sceneIndexToLoad);
        }
    }
}