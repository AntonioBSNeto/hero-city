using UnityEngine;

public class EnemyAttrs : MonoBehaviour
{
    public int life = 100;

    private Renderer enemyRenderer;
    
    void Start()
    {
        // Initialize enemy attributes if needed
        enemyRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Update enemy behavior if needed
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = Color.red;
        }
        Destroy(gameObject);
        killCountController.Pontuacao++;
    }
}