using System;
using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform mainTransform;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Camera customCamera;

    private void LateUpdate()
    {
        if (customCamera == null)
        {
            Debug.LogError("Veuillez assigner la caméra dans l'inspector.");
            return;
        }

        Vector3 camForwardVector = new Vector3(customCamera.transform.forward.x, 0f, customCamera.transform.forward.z);

        // Calcul de l'angle entre la direction de la caméra et le vecteur "vers l'avant" de l'objet
        float signedAngle = Vector3.SignedAngle(camForwardVector, mainTransform.forward, Vector3.up);

        Vector2 animationDirection = Vector2.zero;

        if (signedAngle >= -45f && signedAngle < 45f)
        {
            // Vue de face
            animationDirection = new Vector2(0f, -1f);
        }
        else if (signedAngle >= 45f && signedAngle < 135f)
        {
            // Vue de gauche
            animationDirection = new Vector2(1f, 0f);
        }
        else if ((signedAngle >= 135f && signedAngle <= 180f) || (signedAngle >= -180f && signedAngle < -135f))
        {
            // Vue de dos
            animationDirection = new Vector2(0f, 1f);
        }
        else if (signedAngle >= -135f && signedAngle < -45f)
        {
            // Vue de droite
            animationDirection = new Vector2(-1f, 0f);
        }

        // Appliquer la direction d'animation
        animator.SetFloat("moveX", animationDirection.x);
        animator.SetFloat("moveY", animationDirection.y);
    }
}