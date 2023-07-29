using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 1f;
    public Rigidbody2D rb;
    public int damage = 1;

    bool hit;
    Vector2 startingPoint;
    float currentDistance;
    public GameObject impactEffect;

    private void Awake()
    {
        hit = false;
        startingPoint = transform.position;
        rb.velocity = transform.right * speed;
    }
    void LateUpdate()
    {
        currentDistance = Vector2.Distance(startingPoint, transform.position);
        if (currentDistance >= WeaponRangedPlayerPrefab.range)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit)
            return;
        hit = true;
        CharacterStatus character = collision.GetComponent<CharacterStatus>();
        if (character != null)
        {
            character.TakeDamage(damage);
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
