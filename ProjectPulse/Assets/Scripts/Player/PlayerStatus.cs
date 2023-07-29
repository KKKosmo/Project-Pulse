using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int playerMaxHealth = 100;
    public int playerCurrentHealth;
    public static bool playerAlive;
    public HealthBar healthBar;

    public float knockbackX = 1;
    public float knockbackY = 1;
    bool knockbacked;
    readonly string hitAnimation = "Knockback";
    public float knockbackTime;

    public GameObject deathEffect;
    private void Awake()
    {
        playerCurrentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
        playerAlive = true;
    }
    void Update()
    {
        AnimatePlayer();
    }
    public void TakeDamage(int damage, float direction)
    {
        knockbacked = true;
        playerCurrentHealth -= damage;
        healthBar.SetHealth(playerCurrentHealth);
        if (playerCurrentHealth <= 0)
        {
            Die();
            return;
        }
        PlayerMovement.playerBody.velocity = Vector3.zero;
        if (direction <= 0f)
            PlayerMovement.playerBody.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Impulse);
        else
            PlayerMovement.playerBody.AddForce(new Vector2(-knockbackX, knockbackY), ForceMode2D.Impulse);
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        playerAlive = false;
        Destroy(gameObject);
    }
    void AnimatePlayer()
    {
        if (knockbacked)
        {
            PlayerMovement.anim.SetBool(hitAnimation, true);
            // set knockbackTime depending on the enemy attack not the animation length
            knockbackTime = PlayerMovement.anim.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(Timer(knockbackTime));
            return;
        }
        else
        {
            PlayerMovement.anim.SetBool(hitAnimation, false);
        }
    }
    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        knockbacked = false;
    }
}//class
