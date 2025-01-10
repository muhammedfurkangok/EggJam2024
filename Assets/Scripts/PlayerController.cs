using System.Linq;
using Cysharp.Threading.Tasks;
using ScriptableObjects;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private GameConstants gameConstants;

    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineNoise;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] walkSounds;

    [Header("Info")] [SerializeField] private float currentSpeed;
    [SerializeField] private bool isWaitingForSound;


    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        HandleMouseLook();
        ToggleSound().Forget();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        UpdateCameraNoise();
    }

    // private void HandleInspection()
    // {
    //     if (TabletManager.Instance.IsTabletActive()) return;
    //     if (!PlayerGrabManager.Instance.IsHoldingItem()) return;
    //
    //     if (InputManager.Instance.IsRightClickDown() && !isInspecting) isInspecting = true;
    //     if (InputManager.Instance.IsRightClickUp() && isInspecting) isInspecting = false;
    // }
    //
    // private void HandleThrowing()
    // {
    //     if (isInspecting) return;
    //     if (!PlayerGrabManager.Instance.IsHoldingItem()) return;
    //
    //     if (InputManager.Instance.IsLeftClickDown())
    //     {
    //         PlayerGrabManager.Instance.GetCurrentGrabItem().ThrowItem(transform.forward);
    //         PlayerGrabManager.Instance.OnItemThrown();
    //     }
    // }

    private void HandleMouseLook()
    {
        var mouseSensitivity = gameConstants.playerLookSensitivity;
        var lookDelta = InputManager.Instance.GetLookInput() * (mouseSensitivity * Time.deltaTime);

        var cameraLocalEulerAngles = cinemachineCamera.transform.localEulerAngles;

        var newCameraYAngle = cameraLocalEulerAngles.y + lookDelta.x;

        var newCameraXAngle = cameraLocalEulerAngles.x - lookDelta.y;
        if (newCameraXAngle > 180) newCameraXAngle -= 360;
        newCameraXAngle = Mathf.Clamp(newCameraXAngle, gameConstants.playerMinLook, gameConstants.playerMaxLook);

        cameraLocalEulerAngles.x = newCameraXAngle;
        cameraLocalEulerAngles.y = newCameraYAngle;
        cinemachineCamera.transform.localEulerAngles = cameraLocalEulerAngles;
    }

    private void HandleMovement()
    {
        var moveInput = InputManager.Instance.GetMoveInput();
        var isRunKey = InputManager.Instance.IsRunKey();

        var targetSpeed = moveInput.magnitude > 0
            ? (isRunKey ? gameConstants.playerRunSpeed : gameConstants.playerWalkSpeed)
            : 0f;
        var acceleration = isRunKey ? gameConstants.playerRunAcceleration : gameConstants.playerWalkAcceleration;

        currentSpeed = moveInput.magnitude > 0
            ? Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime)
            : Mathf.MoveTowards(currentSpeed, 0, gameConstants.playerDeceleration * Time.fixedDeltaTime);

        var inputDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        var targetDirection = cinemachineCamera.transform.forward * inputDirection.z +
                              cinemachineCamera.transform.right * inputDirection.x;
        var movement = targetDirection * (currentSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(rigidbody.position + movement);

        var cameraForward = cinemachineCamera.transform.forward;
        cameraForward.y = 0;
        transform.forward = cameraForward;
    }

    private void UpdateCameraNoise()
    {
        var moveInput = InputManager.Instance.GetMoveInput();
        var isRunKey = InputManager.Instance.IsRunKey();

        var targetAmplitude = moveInput.magnitude > 0
            ? isRunKey
                ? gameConstants.playerRunNoiseAmplitude
                : gameConstants.playerWalkNoiseAmplitude
            : gameConstants.playerIdleNoiseAmplitude;

        var targetFrequency = moveInput.magnitude > 0
            ? isRunKey
                ? gameConstants.playerRunNoiseFrequency
                : gameConstants.playerWalkNoiseFrequency
            : gameConstants.playerIdleNoiseFrequency;

        targetAmplitude = gameConstants.playerIdleNoiseAmplitude;
        targetFrequency = gameConstants.playerIdleNoiseFrequency;

        var noiseChangeSpeed =
            isRunKey ? gameConstants.playerRunNoiseChangeSpeed : gameConstants.playerWalkNoiseChangeSpeed;

        cinemachineNoise.AmplitudeGain = Mathf.MoveTowards(cinemachineNoise.AmplitudeGain, targetAmplitude,
            noiseChangeSpeed * Time.fixedDeltaTime);
        cinemachineNoise.FrequencyGain = Mathf.MoveTowards(cinemachineNoise.FrequencyGain, targetFrequency,
            noiseChangeSpeed * Time.fixedDeltaTime);
    }

    private async UniTask ToggleSound()
    {
        if (isWaitingForSound) return;
        if (walkSounds.Length == 0) return;

        if (!audioSource.isPlaying && currentSpeed > 0.4f)
        {
            await PlayNextWalkSound();
        }

        else if (currentSpeed <= 0.4f)
        {
            audioSource.Stop();
        }
    }

    private async UniTask PlayNextWalkSound()
    {
        if (isWaitingForSound) return;

        isWaitingForSound = true;

        var randomIndex = Random.Range(0, walkSounds.Length);
        audioSource.clip = walkSounds[randomIndex];
        audioSource.Play();

        await UniTask.WaitForSeconds(gameConstants.playerWalkingSoundInterval);

        isWaitingForSound = false;
    }
}