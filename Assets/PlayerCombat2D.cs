using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.Cinemachine;

public class PlayerCombat2D : MonoBehaviour, IDamageable
{
    private Health _health;

    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;

    [Header("CAMERA SHAKE")]
    [SerializeField] private Vector3 shakeVelocity = new(0f, -0.5f, 0f);

    [Header("CHROMATIC ABER.")]
    [SerializeField] private float duration = 0.1f;

    [SerializeField] private Volume Volume;


    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += Die;
        _health.OnHealthChanged += TakeDamage;
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    public void TakeDamage(int damage)
    {
        Aberrate();
        ShakeCamera();
    }

    private void Aberrate()
    {
        Volume.sharedProfile.TryGet<ChromaticAberration>(out var component);
        DOTween.To(() => component.intensity.value, x => component.intensity.value = x, 1f, duration / 2).OnComplete(() =>
        {
            DOTween.To(() => component.intensity.value, x => component.intensity.value = x, 0f, duration / 2);
        });
    }


    private void ShakeCamera()
    {
        _cinemachineImpulseSource.DefaultVelocity = shakeVelocity;
        _cinemachineImpulseSource.GenerateImpulse();
    }

}
