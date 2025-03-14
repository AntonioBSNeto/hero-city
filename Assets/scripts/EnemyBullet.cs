using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 20;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the bullet after it hits the player
        }
    }
}