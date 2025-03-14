using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death
        Debug.Log("Player died!");
        Destroy(gameObject);
        // Add additional logic for player death, such as restarting the level
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // Assume the bullet has a script with a damage value
            int damage = collision.gameObject.GetComponent<EnemyBullet>().damage;
            TakeDamage(damage);
            Destroy(collision.gameObject); // Destroy the bullet after it hits the player
        }
    }
}
