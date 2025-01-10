using Cysharp.Threading.Tasks;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Portal linkedPortal;

    [Header("Info")]
    [SerializeField] private bool isPlayerInPortal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInPortal)
        {
            linkedPortal.isPlayerInPortal = true;
            other.transform.position = linkedPortal.transform.position;
            PlayerTeleported().Forget();
        }
    }

    private async UniTask PlayerTeleported()
    {
        linkedPortal.isPlayerInPortal = true;
        await UniTask.WaitForFixedUpdate();
        await UniTask.WaitForFixedUpdate();
        linkedPortal.isPlayerInPortal = false;
    }
}