using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Info")]
    [SerializeField] private bool isPaused;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused) ClosePauseMenu();
        else if (Input.GetKeyDown(KeyCode.Escape)) OpenPauseMenu();
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }
}