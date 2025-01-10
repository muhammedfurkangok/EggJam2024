using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Vector3 originalPosition;

    public static CameraShaker Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        originalPosition = transform.position;
    }

    public async UniTask ShakeCamera(float duration, float magnitude)
    {
        var elapsed = 0.0f;

        while (elapsed < duration)
        {
            var x = Random.Range(-1f, 1f) * magnitude;
            var y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition =
                new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z + y);

            elapsed += Time.deltaTime;

            await UniTask.Yield();
        }

        transform.position = originalPosition;
    }
}