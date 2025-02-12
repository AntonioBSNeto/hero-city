using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f;
    public int damage = 100;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se colidiu com um inimigo e o destr�i
        // if (collision.CompareTag("Enemy"))
        // {
        //     Destroy(collision.gameObject);
        //     Destroy(gameObject);
        // }


        if(collision.CompareTag("Enemy"))
        {
            EnemyAttrs enemy = collision.GetComponent<EnemyAttrs>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        
    }
}
