using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy2 : MonoBehaviour
{
    //float speed = 1f;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject impactEffect;
    bool hit = false;

    public GameObject creator;
    float range;
    int damage;
    float bulletSpeed;
    float staggerDuration;
    float knockback;

    Vector2 startingPoint;
    float currentDistance;

    private void Awake()
    {
        startingPoint = transform.position;
    }
    private void Start()
    {
        range = creator.GetComponent<EnemyMovement>().aggroRange;
        damage = creator.GetComponent<CharacterWeapon>().damage;
        bulletSpeed = creator.GetComponent<WeaponRanged>().bulletSpeed;
        staggerDuration = creator.GetComponent<WeaponRanged>().staggerDuration;
        knockback = creator.GetComponent<CharacterWeapon>().knockback;
        rb.velocity = transform.right * bulletSpeed;
    }
    void LateUpdate()
    {
        currentDistance = Vector2.Distance(startingPoint, transform.position);
        if (currentDistance >= range)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit)
            return;
        hit = true;
        if (collision.CompareTag("Player"))
        {
            CharacterStatus character = collision.GetComponent<CharacterStatus>();
            if (character != null)
            {
                PlayerStatus2 playerStatus = collision.GetComponent<PlayerStatus2>();
                PlayerMovement2 playerMovement2 = collision.GetComponent<PlayerMovement2>();
               character.TakeDamage(collision.attachedRigidbody, damage, transform.rotation.y, knockback, knockback, playerStatus.healthBar, staggerDuration, playerMovement2);
            }
        }
        //make a script for herz status
        if (collision.CompareTag("Herz"))
        {

            CharacterStatus character = collision.GetComponent<CharacterStatus>();
            if (character != null)
            {
                HerzStatus herzStatus = collision.GetComponent<HerzStatus>();
                HerzMovement herzMovement = collision.GetComponent<HerzMovement>();
                character.TakeDamage(collision.attachedRigidbody, damage, transform.rotation.y, knockback, knockback, herzStatus.healthBar, staggerDuration, herzMovement);
            }
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}