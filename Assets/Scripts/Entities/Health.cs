using UnityEngine;

public class Health: MonoBehaviour, IDamageable
{
    public int health = 5;

    public event System.Action OnDeath;
    public event System.Action<int> OnHealthChanged;
    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHealthChanged?.Invoke(damage);
        if (health <= 0)
        {
            Die();
            OnDeath?.Invoke();
        }
    }

    private void Die()
    {
        Debug.Log("This Object died! " + gameObject.name);
    }
}
