using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]NavMeshAgent agent;
    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(new Vector3(0, 0, transform.position.z));
    }
}