using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _attackCollider;
    [Space]

    [SerializeField] Transform shadow;

    public float speed = 2f;

    private bool isAlive = true;
    private bool isAttacking = false;

    private void Awake()
    {
        Health health = GetComponent<Health>();
        health.health = UnityEngine.Random.Range(2,10);
        health.OnDeath += Dead;
    }

    private void OnGlitch()
    {
        agent.isStopped = true;
        _animator.enabled = false;
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        agent.isStopped = false;
        _animator.SetBool("isWalking", true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(target.position, agent.transform.position);
    }

    private async void Update()
    {
        if (Vector2.Distance(target.position, agent.transform.position) <= 0.8f)
        {
            await Attack();
        }
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
            HandleFlip();
        }
    }

    private void HandleFlip()
    {
        if (target.position.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (target.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Dead()
    {
        _animator.SetBool("isWalking", false);
        _animator.SetTrigger("Die");

        agent.isStopped = true;
        agent.enabled = false;

        isAlive = false;

        shadow.gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        _attackCollider.enabled = false;

        SoundManager.Instance.PlayOneShotSound(SoundType.ZombieDeath, 0.5f);

    }

    public void SetPlayer(Transform player)
    {
        target = player.transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isAttacking)
        {
            other.GetComponent<Health>().TakeDamage(1);
        }
    }

    private async UniTask Attack()
    {
        if (isAttacking) return; // Prevent overlapping calls
        isAttacking = true;
        agent.isStopped = true; // Stop the agent
        agent.velocity = Vector3.zero; // Ensure no residual movement
        _animator.SetBool("isAttacking", true);        
        Debug.Log(_animator.GetBool("isAttacking"));
        await UniTask.Delay(700); 
        _animator.SetBool("isAttacking", false); 
        isAttacking = false; 
        agent.isStopped = false;
        Debug.Log("attacked");
    }

    public void EnableCollider() { Debug.Log("EnableCollider"); _attackCollider.enabled = true; Debug.Log(_animator.GetBool("isAttacking"));    }
    public void DisableCollider() { Debug.Log("EnableCollider"); _attackCollider.enabled = false; Debug.Log(_animator.GetBool("isAttacking"));}
}
