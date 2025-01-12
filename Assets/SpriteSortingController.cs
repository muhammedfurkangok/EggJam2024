using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSortingController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform cameraTransform;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        float distanceToCamera = Vector3.Distance(cameraTransform.position, transform.position);

        spriteRenderer.sortingOrder = Mathf.RoundToInt(-distanceToCamera * 100);
    }
}