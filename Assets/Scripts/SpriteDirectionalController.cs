using System;
using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    public float backAngle = 65f;
    public float sideAngle = 65f;
    public Transform mainTransform;
    public Animator animator;
    public SpriteRenderer spriteRenderer;


    private void LateUpdate()
    {
        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        Debug.DrawRay(Camera.main.transform.position, camForwardVector * 5f, Color.magenta);

        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);
        Vector2 animationDirection = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);

        // This changes the side animation based on what side the camera is viewing the slime from
        if (signedAngle < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (angle < backAngle)
        {
            // Back animation
            animationDirection = new Vector2(0f, -1f);
        }
        else if (angle < sideAngle)
        {
            // Side animation, in this case, this is the right animation
            animationDirection = new Vector2(0f, 0f);
        }
        else
        {
            // Front animation
            animationDirection = new Vector2(0f, 1f);
        }

        animator.SetFloat("moveX", animationDirection.x);
        animator.SetFloat("moveY", animationDirection.y);
    }

}
