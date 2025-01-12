using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    public Animator animator;
    public Transform characterTransform;
    public float speedThreshold = 0.1f;

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        // Kamera yönünü al
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;

        // Karakterin bakış yönünü al
        Vector3 characterForward = characterTransform.forward;

        // İki yön arasındaki açıyı hesapla
        float angle = Vector3.SignedAngle(characterForward, camForward, Vector3.up);

        // Blend Tree parametrelerini ayarla
        if (angle > -45 && angle <= 45)
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 1);
        }
        else if (angle > 45 && angle <= 135)
        {
            animator.SetFloat("moveX", 1);
            animator.SetFloat("moveY", 0);
        }
        else if (angle > -135 && angle <= -45)
        {
            animator.SetFloat("moveX", -1);
            animator.SetFloat("moveY", 0);
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", -1);
        }

        // Karakterin hızını hesapla
        // Karakterin hızını hesapla
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

// Speed parametresini ayarla
        animator.SetFloat("speed", speed > speedThreshold ? speed : -1);
    }
}