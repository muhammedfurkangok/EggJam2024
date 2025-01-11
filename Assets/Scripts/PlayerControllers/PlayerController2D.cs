using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController2D : MonoBehaviour
{
    #region Stats

    [Header("MOVEMENT")] public float moveSpeed = 5f;
    public float stopDistance = 0.1f;
    public float dashSpeed = 20f;
    public float maxDashRange = 5f; // Dash için max mesafe

    [Header("ATTACK")] public bool canAttack = false;
    public GameObject hitParticlePrefab;
    public LayerMask enemyLayer;
    public CinemachineCamera shakeCam;

    #endregion
    [Header("CAM SHAKE")]
    [SerializeField]
    private Vector3 shakeVelocity = new(0f, -0.3f, 0f);

    [Header("FRAME FREEZE")]
    [SerializeField]
    private float frameFreezeDuration = 0.1f;
    [SerializeField] private float freezeStrenght = 0.1f;


    [Header("ANIMATOR")]
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleCollider;
    private bool _isDashing;
    private bool _isMouseOver;

    private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

    }

    private void Update()
    {
        // Mouse'a tıklanırsa dash yap
        if (Input.GetMouseButtonDown(0) && !_isDashing && !_isMouseOver)
        {
            Dash();
        }

        // Mouse'un karakterin üstünde olup olmadığını kontrol et
        CheckMouseOver();

        // Karakterin yönünü kontrol et (Flip)
        HandleFlip();
    }

    private void FixedUpdate()
    {
        if (_isDashing || _isMouseOver) return;

        // Karakteri mouse pozisyonuna doğru hareket ettir
        MoveTowardsMouse();
    }

    private void MoveTowardsMouse()
    {
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        if (distanceToTarget > stopDistance)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            _rb.linearVelocity = direction * moveSpeed;
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            _animator.SetBool("isWalking", false);
        }
    }

    private void Dash()
    {
        _isDashing = true;
        canAttack = true; // Dash sırasında saldırı aktif

        // Mouse pozisyonunu al ve dash yönünü hesapla
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        // Dash mesafesini hesapla (maxDashRange kadar sınırlı)
        float dashDistance = maxDashRange;

        // Dash pozisyonunu belirle
        Vector2 dashTargetPosition = (Vector2)transform.position + dashDirection * dashDistance;

        // Dash yap
        StartCoroutine(DashCoroutine(dashTargetPosition));
    }

    private IEnumerator DashCoroutine(Vector2 targetPosition)
    {
        float dashDuration = Vector2.Distance(transform.position, targetPosition) / dashSpeed;
        Vector2 startPosition = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            _rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / dashDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rb.MovePosition(targetPosition);
        StopDash();
    }

    private void StopDash()
    {
        _isDashing = false;
        _rb.linearVelocity = Vector2.zero;
        canAttack = false; 
    }

    private void CheckMouseOver()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_capsuleCollider.OverlapPoint(mousePos))
        {
            _isMouseOver = true;
            _rb.linearVelocity = Vector2.zero;
            _animator.SetBool("isWalking", false);
        }
        else
        {
            _isMouseOver = false;
        }
    }

    private void HandleFlip()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Eğer mouse karakterin solundaysa flip yap
        if (mousePosition.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }
        else if (mousePosition.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        }
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDashing && canAttack && enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)))
        {
            collision.GetComponent<IDamageable>()?.TakeDamage(1);
            ShakeCamera();
            await FreezeFrame();
            Vector2 hitDirection = (collision.transform.position - transform.position).normalized;
            GameObject hitParticle = Instantiate(hitParticlePrefab, collision.transform.position, Quaternion.identity);
            hitParticle.transform.GetComponent<Animator>().SetTrigger("Slice");
            Destroy( hitParticle, 0.58f);

            float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
            hitParticle.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Debug.Log("Enemy hit!");

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        canAttack = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canAttack = true;
    }

    private async UniTask FreezeFrame()
    {
        Time.timeScale = freezeStrenght;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        await UniTask.WaitForSeconds(frameFreezeDuration, ignoreTimeScale: true);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
    private void ShakeCamera()
    {
        _cinemachineImpulseSource.DefaultVelocity = shakeVelocity;
        _cinemachineImpulseSource.GenerateImpulse();
    }

}
