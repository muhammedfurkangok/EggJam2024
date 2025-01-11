using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [SerializeField] public Texture2D defaultCursor;
    [SerializeField] public Texture2D hoverCursor;
    [SerializeField] private Vector2 hotspot = Vector2.zero;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetCursor(defaultCursor, hotspot, cursorMode);
    }

    public void SetCursor(Texture2D cursorTexture, Vector2 cursorHotspot, CursorMode mode = CursorMode.Auto)
    {
        Cursor.SetCursor(cursorTexture, cursorHotspot, mode);
    }

    public void SetCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
    }
    public void SetCursorLock(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}