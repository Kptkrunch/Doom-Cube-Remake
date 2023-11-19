using Controllers;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthController.contPHealth.TakeDamage(damage);
        }
    }
}
