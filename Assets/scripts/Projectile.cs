using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;
    public int damage = 100;
    public float speed = 10f;
    private void OnEnable()
    {
        // Configura o tempo de vida da bala
        Invoke("DeactivateBullet", lifetime);
    }

    private void Update()
    {
        // Movimenta a bala
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void DeactivateBullet()
    {
        // Devolve a bala ao pool
        SingleBulletPoolManager.Instance.ReturnBullet(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyAttrs enemy = collision.GetComponent<EnemyAttrs>();
            if (enemy != null)
            {
                SingleBulletPoolManager.Instance.ReturnBullet(gameObject);
                enemy.TakeDamage(damage);
            }
        }
    }
}
