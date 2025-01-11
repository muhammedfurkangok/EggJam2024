using UnityEngine;

public class Health: MonoBehaviour, IDamageable
{
    public int health = 5;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("This Object died!" + gameObject.name);
        Destroy(gameObject);
    }
}
