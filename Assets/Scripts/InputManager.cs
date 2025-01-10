using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Info")]
    public bool isInputOverride;
    public Vector2 overrideMoveInput;
    public Vector2 overrideLookInput;
    [SerializeField] private bool overrideInteractKeyDown;
    [SerializeField] private bool overrideRightClickDown;
    [SerializeField] private bool overrideRightClickUp;
    [SerializeField] private bool overrideLeftClickDown;
    [SerializeField] private bool overrideTabletKeyDown;

    private PlayerInputActions playerInputActions;

    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    public Vector2 GetMoveInput() => isInputOverride ? overrideMoveInput : playerInputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 GetLookInput() => isInputOverride ? overrideLookInput : playerInputActions.Player.Look.ReadValue<Vector2>();
    public bool IsRunKey() => false;//playerInputActions.Player.Run.IsInProgress();
    public bool IsInteractKeyDown() => isInputOverride ? overrideInteractKeyDown : playerInputActions.Player.Interact.WasPressedThisFrame();
    public bool IsRightClickDown() => isInputOverride ? overrideRightClickDown : playerInputActions.Player.RightClick.WasPressedThisFrame();
    public bool IsRightClickUp() => isInputOverride ? overrideRightClickUp : playerInputActions.Player.RightClick.WasReleasedThisFrame();
    public bool IsLeftClickDown() => isInputOverride ? overrideLeftClickDown : playerInputActions.Player.LeftClick.WasPressedThisFrame();
    public bool IsTabletKeyDown() => isInputOverride ? overrideTabletKeyDown : playerInputActions.Player.Tablet.WasPressedThisFrame();

    public async UniTask OverrideInteractKeyDown()
    {
        overrideInteractKeyDown = true;
        await UniTask.Yield();
        overrideInteractKeyDown = false;
    }

    public async UniTask OverrideRightClickDown()
    {
        overrideRightClickDown = true;
        await UniTask.Yield();
        overrideRightClickDown = false;
    }

    public async UniTask OverrideRightClickUp()
    {
        overrideRightClickUp = true;
        await UniTask.Yield();
        overrideRightClickUp = false;
    }

    public async UniTask OverrideLeftClickDown()
    {
        overrideLeftClickDown = true;
        await UniTask.Yield();
        overrideLeftClickDown = false;
    }

    public async UniTask OverrideTabletKeyDown()
    {
        overrideTabletKeyDown = true;
        await UniTask.Yield();
        overrideTabletKeyDown = false;
    }
}