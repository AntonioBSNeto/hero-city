using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // O prefab do proj�til
    public Transform firePoint; // O ponto de spawn do proj�til
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Dispara quando apertar espa�o
        {
            Shoot();
        }
    }

    void Shoot()
    {
         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        GameObject bullet = SingleBulletPoolManager.Instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * projectileSpeed, ForceMode2D.Impulse); // Move o projétil
    }
}
