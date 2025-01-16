using UnityEngine;

public class PlayerHealtVisual : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Animator[] _animators;

    int currentHealth;

    private void Awake()
    {
        playerHealth.OnHealthChanged += UpdateHealthVisual;
        currentHealth=playerHealth.health;
    }

    public void UpdateHealthVisual(int change)
    {
        currentHealth -= change;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            return;
        }
        _animators[playerHealth.health - change + 1].SetTrigger("healthTrigger");
    }

    private void OnDestroy()
    {
        playerHealth.OnHealthChanged -= UpdateHealthVisual;
    }
}
