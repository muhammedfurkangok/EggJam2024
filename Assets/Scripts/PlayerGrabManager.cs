using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameConstants gameConstants;
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform holdPosition;
    [SerializeField] private Transform inspectPosition;

    [Header("Info")]
    [SerializeField] private GrabItem lookingGrabItem;
    [SerializeField] private GrabItem currentGrabItem;

    public bool IsHoldingItem() => currentGrabItem != null;
    public Transform GetHoldPosition() => holdPosition;
    public Transform GetInspectPosition() => inspectPosition;
    public GrabItem GetCurrentGrabItem() => currentGrabItem;
    public void OnItemThrown() => currentGrabItem = null;

    public static PlayerGrabManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var crosshairRay = mainCamera.ScreenPointToRay(crosshairImage.rectTransform.position);

        if (Physics.Raycast(crosshairRay, out var crosshairHit, gameConstants.grabRange, gameConstants.grabLayer))
        {
            lookingGrabItem = crosshairHit.collider.GetComponent<GrabItem>();
        }

        else
        {
            lookingGrabItem = null;
        }

        UpdateCrosshairAlpha();

        if (!InputManager.Instance.IsInteractKeyDown()) return;

        if (currentGrabItem != null)
        {
            currentGrabItem.SetTarget(null);
            currentGrabItem = null;
        }

        else if (lookingGrabItem != null)
        {
            currentGrabItem = lookingGrabItem;

            if (currentGrabItem.enabled == false)
            {
                currentGrabItem = null;
                return;
            }

            currentGrabItem.SetTarget(holdPosition);
        }
    }

    private void UpdateCrosshairAlpha()
    {
        var crosshairImageColor = crosshairImage.color;
        crosshairImageColor.a = lookingGrabItem != null ? 1f : 0.25f;
        crosshairImage.color = crosshairImageColor;
    }
}