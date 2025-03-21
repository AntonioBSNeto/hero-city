using System.Collections;
// Copyright © 2014 Pixelnest Studio
// This file is subject to the terms and conditions defined in
// file 'LICENSE.md', which is part of this source code package.
using UnityEngine;

namespace Pixelnest.BulletML.Demo
{
  /// <summary>
  /// Move the player ship with arrows
  /// </summary>
  public class DemoPlayerController : MonoBehaviour
  {
    public float speed = 30f;
    public float maxSpeed = 10f;

    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;
    public GameObject projectilePrefab3;

    public int maxHP = 20; // HP máximo do jogador
    private int currentHP;

    private Vector2 movement;
    private int damageTaken;
    private DemoFightScript demo;
    private Rigidbody2D rbody2d;

    private float shootCooldown2 = 1f; // Intervalo maior para o segundo tipo de tiro
    private float shootCooldown3 = 0.1f; // Intervalo curto para o terceiro tipo de tiro (laser)
    private float shootTimer2 = 0f;
    private float shootTimer3 = 0f;

    private int currentWeapon = 1; // 1: projectilePrefab1, 2: projectilePrefab2, 3: projectilePrefab3

    public float projectileSpeed1 = 25f;
    public float projectileSpeed2 = 10f; // Velocidade mais lenta para o segundo tipo de tiro
    public float projectileSpeed3 = 25f;

    public int projectileDamage1 = 2;
    public int projectileDamage2 = 8; // Velocidade mais lenta para o segundo tipo de tiro
    public int projectileDamage3 = 1;

    private PowerUpManager powerUpManager;
    private GameSceneManager sceneManager;

    void Awake()
    {
      damageTaken = 0;
      currentHP = maxHP;
      demo = FindObjectOfType<DemoFightScript>();
      powerUpManager = FindObjectOfType<PowerUpManager>();
      sceneManager = FindObjectOfType<GameSceneManager>();

      rbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      float inputX = Input.GetAxis("Horizontal");
      float inputY = Input.GetAxis("Vertical");

      movement = new Vector2(
        inputX * speed,
        inputY * speed
      );

      movement = new Vector2(
        Mathf.Clamp(movement.x, -maxSpeed, maxSpeed),
        Mathf.Clamp(movement.y, -maxSpeed, maxSpeed)
      );

      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
        currentWeapon = 1;
      }
      else if (Input.GetKeyDown(KeyCode.Alpha2))
      {
        currentWeapon = 2;
      }
      else if (Input.GetKeyDown(KeyCode.Alpha3))
      {
        currentWeapon = 3;
      }

      bool shoot = Input.GetButtonDown("Fire1");

      if (shoot)
      {
        Shoot();
      }

      shootTimer2 -= Time.deltaTime;
      shootTimer3 -= Time.deltaTime;
    }

    void FixedUpdate()
    {
      rbody2d.linearVelocity = movement;
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
      BulletScript bullet = otherCollider.GetComponent<BulletScript>();

      if (bullet != null)
      {
        TakeDamage(1); // Cada bala causa 1 de dano

        StartCoroutine(FlashRed());

        Destroy(bullet.gameObject);
      }

      PowerUp powerUp = otherCollider.GetComponent<PowerUp>();
      if (powerUp != null)
      {
        powerUpManager.ApplyPowerUp(this, powerUp);
        Destroy(powerUp.gameObject);
      }
    }

    void OnGUI()
    {
      if (demo.showGUI)
      {
        GUI.Label(new Rect(5, 5, 150, 50), "Player HP: " + currentHP);
      }
    }

    private void Shoot()
    {
      GameObject shot = null;
      float shotSpeed = 0f;

      switch (currentWeapon)
      {
        case 1:
          shot = Instantiate(projectilePrefab1);
          shot.GetComponent<DemoPlayerShotScript>().damage = projectileDamage1;
          shotSpeed = projectileSpeed1;
          break;
        case 2:
          if (shootTimer2 <= 0f)
          {
            shot = Instantiate(projectilePrefab2);
            shot.GetComponent<DemoPlayerShotScript>().damage = projectileDamage2;
            shotSpeed = projectileSpeed2;
            shootTimer2 = shootCooldown2;
          }
          break;
        case 3:
          if (shootTimer3 <= 0f)
          {
            shot = Instantiate(projectilePrefab3);
            shot.GetComponent<DemoPlayerShotScript>().damage = projectileDamage3;
            shot.GetComponent<DemoPlayerShotScript>().isHoming = true;
            shotSpeed = projectileSpeed3;
            shootTimer3 = shootCooldown3;
          }
          break;
      }

      if (shot != null)
      {
        shot.transform.position = this.transform.position;
        DemoPlayerShotScript shotScript = shot.GetComponent<DemoPlayerShotScript>();
        shotScript.speed = new Vector2(shotSpeed, 0);
      }
    }

    public void IncreaseSpeed(float amount)
    {
      speed += amount;
    }

    public void RecoverHealth(float amount)
    {
      currentHP = Mathf.Min(currentHP + (int)amount, maxHP);
    }

    public void IncreaseDamage(float amount)
    {
      projectileDamage1 += (int)amount;
      projectileDamage2 += (int)amount;
      projectileDamage3 += (int)amount;
    }

    private void TakeDamage(int damage)
    {
      currentHP -= damage;
      damageTaken++;

      if (currentHP <= 0)
      {
        Die();
      }
    }

    private void Die()
    {
      // Handle player death
      Debug.Log("Player died!");
      Destroy(gameObject);
      // Transitar para a cena HighScore após 3 segundos
      sceneManager.LoadHighScoreAfterDelay(3f);
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