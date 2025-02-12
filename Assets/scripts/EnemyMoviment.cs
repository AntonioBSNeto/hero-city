using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 1.5f; // Speed at which the enemy moves towards the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Optionally, find the player by tag if not assigned in the inspector
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move the enemy towards the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}