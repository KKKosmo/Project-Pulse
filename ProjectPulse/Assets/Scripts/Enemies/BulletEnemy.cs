using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public int damage = 20;

    public GameObject impactEffect;
    bool hit = false;

    Vector2 startingPoint;
    float currentDistance;

    private void Awake()
    {
        startingPoint = transform.position;
        rb.velocity = transform.right * speed;
    }
    void LateUpdate()
    {
        currentDistance = Vector2.Distance(startingPoint, transform.position);
        if (currentDistance >= Enemy.aggroRange)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit)
            return;
        hit = true;
            if (collision == PlayerMovement.boxCollider2D || collision == PlayerMovement.capsuleCollider2D)
            {
                PlayerStatus player = collision.GetComponent<PlayerStatus>();
            if (player != null)
                {
                player.TakeDamage(damage, transform.position.z);
            }
            }
            //make a script for herz status
            else if (collision == Herz.boxCollider2D || collision == Herz.capsuleCollider2D)
            {
                Herz herz = collision.GetComponent<Herz>();
                if (herz != null)
                {

                    herz.TakeDamage(damage, transform.rotation.y);
                }
            }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
