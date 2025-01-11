using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}
