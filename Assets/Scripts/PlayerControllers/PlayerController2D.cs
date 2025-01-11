using System;
using UnityEditor.ShaderGraph;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
    #region Stats
    [Header("LAYERS")]
    [Tooltip("Set this to the layer your player is on")]
    public LayerMask PlayerLayer;

    [Header("INPUT")]
    [Tooltip("Makes all Input snap to an integer. Prevents gamepads from walking slowly. Recommended value is true to ensure gamepad/keybaord parity.")]
    public bool SnapInput = true;

    [Tooltip("Minimum input required before you mount a ladder or climb a ledge. Avoids unwanted climbing using controllers"), Range(0.01f, 0.99f)]
    public float VerticalDeadZoneThreshold = 0.3f;

    [Tooltip("Minimum input required before a left or right is recognized. Avoids drifting with sticky controllers"), Range(0.01f, 0.99f)]
    public float HorizontalDeadZoneThreshold = 0.1f;

    [Header("MOVEMENT")]
    [Tooltip("The top horizontal movement speed")]
    public float MaxSpeed = 14;

    [Tooltip("The player's capacity to gain horizontal speed")]
    public float Acceleration = 120;

    [Tooltip("The pace at which the player comes to a stop")]
    public float GroundDeceleration = 60;

    #endregion
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private FrameInput2D _frameInput;
    private Vector2 _frameVelocity;
    private bool _cachedQueryStartInColliders;
    private Controls _controls;

    [SerializeField] private ParticleSystem DashParticles;

    #region Interface

    public Vector2 FrameInput => _frameInput.Move;

    public void Reset()
    {
        _rb.linearVelocity = new Vector2(0, 0);
    }

    #endregion
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _controls = new Controls();
        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }
    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {

        _frameInput = new FrameInput2D
        {
            Move = _controls._2D.Move.ReadValue<Vector2>()
        };
        if (SnapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
        }
    }


    private void FixedUpdate()
    {
        _frameVelocity = _rb.linearVelocity;

        CheckCollisions();
        HandleDirection();
        ApplyMovement();
    }

    #region Collisions

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        bool isMaxX() => Mathf.Abs(_frameVelocity.x) > MaxSpeed && Mathf.Sign(_frameInput.Move.x) == Mathf.Sign(_frameVelocity.x);
        bool isMaxY() => Mathf.Abs(_frameVelocity.y) > MaxSpeed && Mathf.Sign(_frameInput.Move.y) == Mathf.Sign(_frameVelocity.y);

        if (_frameInput.Move.x == 0 || isMaxX())
        {
            var deceleration = GroundDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * MaxSpeed, Acceleration * Time.fixedDeltaTime);
        }

        if (_frameInput.Move.y == 0 || isMaxY())
        {
            var deceleration = GroundDeceleration;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, _frameInput.Move.y * MaxSpeed, Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    private void ApplyMovement() => _rb.linearVelocity = _frameVelocity;
}
public struct FrameInput2D
{
    public Vector2 Move;
}