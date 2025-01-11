using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]NavMeshAgent agent;
    [SerializeField] Transform target;

    public float speed = 2f;
    private async void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;

        while(true)
        {
            await UniTask.WaitForSeconds(0.2f);
            agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
        }
    }
}