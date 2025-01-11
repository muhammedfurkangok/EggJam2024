using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private Animator _animator;

    public float speed = 2f;

    private async void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        _animator.SetBool("isWalking", true);

        while (true)
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
    }
}