using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private Animator _animator;

    [Space]

    [SerializeField] Transform shadow;

    public float speed = 2f;

    private bool isAlive = true;

    private void Awake()
    {
        Health health = GetComponent<Health>();
        health.OnDeath += Dead;
    }

    private async void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        _animator.SetBool("isWalking", true);

        while (true && isAlive)
        {
            await UniTask.WaitForSeconds(0.2f);
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
        isAlive = false;
        ShadowFixOnDeath();
        GetComponent<Collider2D>().enabled = false;
    }

    private void ShadowFixOnDeath()
    {
        shadow.gameObject.SetActive(false); // or strecth the shadow to accomodate.
    }
}
