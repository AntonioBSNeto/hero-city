using UnityEngine;

public class FloatingPowerUp : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float floatAmplitude = 0.5f;
    private Transform player;
    private Vector3 startPosition;
    private float floatTimer;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        startPosition = transform.position;
        floatTimer = Random.Range(0f, 2f * Mathf.PI); // Randomize the start of the floating effect
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * floatSpeed * Time.deltaTime;

            // Apply floating effect
            floatTimer += Time.deltaTime;
            float floatOffset = Mathf.Sin(floatTimer) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, startPosition.y + floatOffset, transform.position.z);
        }
    }
}