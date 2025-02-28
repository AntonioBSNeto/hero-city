using System.Collections.Generic;
using UnityEngine;

public class SingleBulletPoolManager : MonoBehaviour
{
    public static SingleBulletPoolManager Instance; // Singleton para acesso global

    private Queue<GameObject> bulletPool; // Pool de balas atual
    private GameObject currentBulletPrefab; // Prefab da bala atual

    private void Awake()
    {
        // Configura o Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializa o pool vazio
        bulletPool = new Queue<GameObject>();
    }

    // Método para configurar o pool com um novo tipo de bala
    public void SetBulletType(GameObject bulletPrefab, int poolSize)
    {
        // Limpa o pool atual (se houver)
        foreach (var bullet in bulletPool)
        {
            Destroy(bullet); // Destrói as balas antigas
        }
        bulletPool.Clear();

        // Define o novo prefab de bala
        currentBulletPrefab = bulletPrefab;

        // Preenche o pool com balas inativas
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(currentBulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    // Método para obter uma bala do pool
    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // Se o pool estiver vazio, cria uma nova bala
            GameObject newBullet = Instantiate(currentBulletPrefab);
            return newBullet;
        }
    }

    // Método para devolver uma bala ao pool
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}