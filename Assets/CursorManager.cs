using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [SerializeField] private Texture2D defaultCursor;
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

        // Set the default cursor at the start
        SetCursor(defaultCursor, hotspot, cursorMode);
    }

    /// <summary>
    /// Changes the cursor to the given texture.
    /// </summary>
    public void SetCursor(Texture2D cursorTexture, Vector2 cursorHotspot, CursorMode mode = CursorMode.Auto)
    {
        Cursor.SetCursor(cursorTexture, cursorHotspot, mode);
    }

    /// <summary>
    /// Shows or hides the cursor.
    /// </summary>
    public void SetCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
    }

    /// <summary>
    /// Locks or unlocks the cursor.
    /// </summary>
    public void SetCursorLock(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
}