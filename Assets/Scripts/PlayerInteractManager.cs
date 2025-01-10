using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image crosshairImage;
    [SerializeField] private GameConstants gameConstants;
    [SerializeField] private Camera mainCamera;

    public static PlayerInteractManager Instance;

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
        try
        {
            if (!InputManager.Instance.IsInteractKeyDown()) return;

            var crosshairRay = mainCamera.ScreenPointToRay(crosshairImage.rectTransform.position);
            if (!Physics.Raycast(crosshairRay, out var crosshairHit, gameConstants.playerInteractRange, gameConstants.playerInteractLayer)) return;

            var door = crosshairHit.collider.transform.parent.parent.GetComponent<Door>();
            if (door == null)
            {
                door = crosshairHit.collider.transform.parent.parent.parent.GetComponent<Door>();
                if (door == null) return;
            }

            door.ToggleDoor();
        }

        catch { }
    }
}
