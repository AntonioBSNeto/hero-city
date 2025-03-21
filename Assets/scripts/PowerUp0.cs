using UnityEngine;

public class PowerUp0 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add your power-up logic here
            Debug.Log("Power-up collected by player!");
            Destroy(gameObject);
        }
    }
}