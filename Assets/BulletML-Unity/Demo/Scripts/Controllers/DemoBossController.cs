using UnityEngine;
using System.Collections;

namespace Pixelnest.BulletML.Demo
{
  public class DemoBossController : MonoBehaviour
  {
    public int hp = 100;
    public float speed = 2f;
    public GameObject[] powerUpPrefabs; // Array de prefabs de power-ups
    public AudioClip spawnSound; // Som de spawn do chefe
    public GameObject explosionVFX; // Prefab do VFX de explosão
    public AudioClip explosionSound; // Som de explosão
    public AudioClip impactSound; // Som de impacto
    
    private Vector3 movementTarget;
    private DemoFightScript demo;
    private BossManager bossManager;
    private GameSceneManager sceneManager;

    private Collider2D col2d;

    void Awake()
    {
      col2d = GetComponent<Collider2D>();
    }

    void Start()
    {
      NewMoveTarget();
      demo = FindObjectOfType<DemoFightScript>();
      bossManager = FindObjectOfType<BossManager>();
      sceneManager = FindObjectOfType<GameSceneManager>();

      // Reproduzir o som de spawn
      PlaySpawnSound();
    }

    private void PlaySpawnSound()
    {
      if (spawnSound != null)
      {
        AudioManager.instance.PlaySoundEffect(spawnSound);
      }
    }

    void Update()
    {
      if (col2d.OverlapPoint(movementTarget))
      {
        NewMoveTarget();
      }

      Vector2 direction = (movementTarget - this.transform.position);
      direction.Normalize();
      this.transform.Translate(direction * speed * Time.deltaTime);
    }

    private void NewMoveTarget()
    {
      movementTarget = new Vector3(
        Random.Range(0.5f, 0.9f),
        Random.Range(0.1f, 0.9f),
        0);

      movementTarget = Camera.main.ViewportToWorldPoint(movementTarget);
      movementTarget.z = 0;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
      // Collision with player projectile
      DemoPlayerShotScript playerShot = otherCollider.GetComponent<DemoPlayerShotScript>();

      if (playerShot != null)
      {
        hp -= playerShot.damage;

        Destroy(playerShot.gameObject);

        // Reproduzir VFX e som de impacto
                if (AudioManager.instance != null && impactSound != null)
        {
          AudioManager.instance.PlaySoundEffect(impactSound);
        }

        // Flash red
        StartCoroutine(FlashRed());

        if (hp <= 0)
        {
          StartCoroutine(HandleBossDeath());
        }
      }
    }

    private IEnumerator HandleBossDeath()
    {
      yield return new WaitForSeconds(0.05f); // Pequeno delay para o efeito de flash

      // Reproduzir VFX e som de explosão
      if (explosionVFX != null)
      {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
      }
      if (AudioManager.instance != null && explosionSound != null)
      {
        AudioManager.instance.PlaySoundEffect(explosionSound);
      }

      DropPowerUp();

      if (bossManager != null)
      {
        bossManager.OnBossDeath(); // Notificar o BossManager
      }

      ScoreManager.instance.AddScore(100); // Adiciona 100 pontos ao score

      Destroy(this.gameObject);
    }

    private void DropPowerUp()
    {
      if (powerUpPrefabs.Length > 0)
      {
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject powerUp = Instantiate(powerUpPrefabs[randomIndex], transform.position, Quaternion.identity);
        powerUp.AddComponent<FloatingPowerUp>();
      }
    }

    void OnGUI()
    {
      if (demo.showGUI)
      {
        GUI.Label(new Rect(5, 25, 150, 50), "Boss Health: " + hp);
      }
    }

    private IEnumerator FlashRed()
    {
      SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();

      sprite.color = Color.red;

      yield return new WaitForSeconds(0.05f);

      sprite.color = Color.white;

      yield return null;
    }
  }
}