using System;
using UnityEngine;

public class PlayerCombat2D : MonoBehaviour, IDamageable
{
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += Die;
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    public void TakeDamage(int damage)
    {

    }

}
