using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameConstants gameConstants;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider collider;

    [Header("Parameters")]
    [SerializeField] private GrabItemType grabItemType;
    [SerializeField] private GrabItemColor grabItemColor;

    [Header("Info")]
    [SerializeField] private Transform target;
    [SerializeField] private bool isBeingInspected;
    [SerializeField] private bool isSnapped;
    [SerializeField] private GrabItemPosition grabItemPosition;

    private Tween enterInspectTween;
    private Tween exitInspectTween;
    private Tween exitInspectRotationTween;
    private Tween snapTween;
    private Tween snapRotationTween;

    public GrabItemType GetGrabItemType() => grabItemType;
    public bool IsBeingGrabbed() => target != null;
    public bool GetIsBeingInspected() => isBeingInspected;
    public float GetRigidbodyVelocityMagnitude() => rigidbody.linearVelocity.magnitude;
    public Collider GrabItemPositionOnly_GetCollider() => collider;
    public bool IsSnapped() => isSnapped;

    public void SetTarget(Transform target)
    {
        if (isSnapped) return;
        this.target = target;

        if (target != null)
        {
            if (isSnapped) grabItemPosition.SetCompleted(false);

            isSnapped = false;
            grabItemPosition = null;
            rigidbody.useGravity = false;
            collider.excludeLayers = gameConstants.grabItemExcludeLayers;
        }

        else
        {
            rigidbody.useGravity = true;
            collider.excludeLayers = 0;
        }
    }

    public void ThrowItem(Vector3 direction)
    {
        SetTarget(null);
        rigidbody.AddForce(direction * gameConstants.grabThrowForce, ForceMode.Impulse);
    }

    private void Update()
    {
        if (target == null) return;
        if (InputManager.Instance.IsRightClickDown() && !isBeingInspected) EnterInspectMode();
        if (InputManager.Instance.IsRightClickUp() && isBeingInspected) ExitInspectMode();

        if (isBeingInspected && target != null) Inspect();
    }

    private void FixedUpdate()
    {
        if (isBeingInspected) return;

        if (target == null)
        {
            rigidbody.linearVelocity -= rigidbody.linearVelocity.normalized * (gameConstants.grabReleaseDeceleration * Time.fixedDeltaTime);
            return;
        }

        rigidbody.isKinematic = false;

        var direction = target.position - transform.position;
        var distance = direction.magnitude;
        direction.Normalize();

        var desiredLinearVelocity = direction * (gameConstants.grabForce * distance);
        rigidbody.linearVelocity = desiredLinearVelocity;

        var torqueAxis = Vector3.Cross(Vector3.up, direction).normalized;
        var angularMagnitude = desiredLinearVelocity.magnitude * gameConstants.grabAngularForce;
        rigidbody.angularVelocity = torqueAxis * angularMagnitude;
    }

    private void EnterInspectMode()
    {
        isBeingInspected = true;
        rigidbody.isKinematic = true;

        enterInspectTween?.Kill();
        exitInspectTween?.Kill();
        exitInspectRotationTween?.Kill();
        snapTween?.Kill();
        snapRotationTween?.Kill();

        enterInspectTween = transform.DOMove(PlayerGrabManager.Instance.GetInspectPosition().position,
                gameConstants.grabItemEnterInspectDuration)
            .SetEase(gameConstants.grabItemEnterInspectEase);
    }

    private void ExitInspectMode()
    {
        isBeingInspected = false;
        rigidbody.isKinematic = false;

        enterInspectTween?.Kill();
        exitInspectTween?.Kill();
        exitInspectRotationTween?.Kill();
        snapTween?.Kill();
        snapRotationTween?.Kill();

        exitInspectTween = transform.DOMove(PlayerGrabManager.Instance.GetHoldPosition().position,
                gameConstants.grabItemExitInspectDuration)
            .SetEase(gameConstants.grabItemExitInspectEase);
    }

    private void Inspect()
    {
        if (enterInspectTween != null && enterInspectTween.IsActive()) return;
        if (exitInspectTween != null && exitInspectTween.IsActive()) return;
        if (exitInspectRotationTween != null && exitInspectRotationTween.IsActive()) return;

        var mouseX = InputManager.Instance.GetLookInput().x * gameConstants.playerInspectSensitivity * Time.deltaTime;
        var mouseY = InputManager.Instance.GetLookInput().y * gameConstants.playerInspectSensitivity * Time.deltaTime;

        var rotationX = Quaternion.AngleAxis(-mouseX, Vector3.up);
        var rotationY = Quaternion.AngleAxis(mouseY, Vector3.right);

        transform.rotation = rotationX * rotationY * transform.rotation;
    }

    public void TrySnap(GrabItemPosition grabItemPosition)
    {
        if (isSnapped) return;
        if (grabItemPosition.IsCompleted()) return;
        if (rigidbody.linearVelocity.magnitude > gameConstants.grabItemSnapMaxVelocity) return;
        if (grabItemPosition.GetNeededGrabItemType() != grabItemType) return;

        if (IsBeingGrabbed())
        {
            grabItemPosition.PlayColorChangeAnimation(false);
            return;
        }

        isSnapped = true;
        this.grabItemPosition = grabItemPosition;

        snapTween?.Kill();
        snapRotationTween?.Kill();

        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        snapTween = transform.DOMove(grabItemPosition.transform.position, gameConstants.grabItemSnapDuration)
            .SetEase(gameConstants.grabItemSnapEase);

        snapRotationTween = transform.DORotateQuaternion(grabItemPosition.transform.rotation, gameConstants.grabItemSnapDuration)
            .SetEase(gameConstants.grabItemSnapRotationEase);

        if (grabItemColor == grabItemPosition.GetNeededGrabItemColor())
            grabItemPosition.SetCompleted(true);

        rigidbody.isKinematic = true;
    }
}