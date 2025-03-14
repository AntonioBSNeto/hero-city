// Copyright © 2014 Pixelnest Studio
// This file is subject to the terms and conditions defined in
// file 'LICENSE.md', which is part of this source code package.
using UnityEngine;
using System.Collections;

namespace Pixelnest.BulletML.Demo
{
  public class DemoPlayerShotScript : MonoBehaviour
  {
    public Vector2 speed = Vector2.zero;
    public int damage = 1;
    public bool isHoming = false;
    private Transform target;

    private Rigidbody2D rbody2d;
    private SpriteRenderer sprite;

    void Awake()
    {
      rbody2d = GetComponent<Rigidbody2D>();
      sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
      if (isHoming)
      {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
      }
    }

    void Update()
    {
      // Destroy when outside the screen
      if (sprite != null && sprite.isVisible == false)
      {
        Destroy(this.gameObject);
      }

      if (isHoming && target != null)
      {
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();
        transform.Translate(direction * speed.magnitude * Time.deltaTime);
      }
      else
      {
        transform.Translate(speed * Time.deltaTime);
      }
    }

    void FixedUpdate()
    {
      rbody2d.linearVelocity = speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Enemy"))
      {
        // Apply damage to the enemy
        EnemyAttrs enemy = other.GetComponent<EnemyAttrs>();
        if (enemy != null)
        {
          enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
      }
    }
  }
}