using UnityEngine;

public class Herz : MonoBehaviour
{
    public int herzMaxHealth = 100;
    public int herzCurrentHealth;
    public static bool herzIsAlive;
    public HerzHealthBar healthBar;
    public GameObject deathEffect;

    public Rigidbody2D rb;
    public float moveSpeed;
    public float crouchSpeedMultiplier = 0.65f;
    bool m_FacingRight = true;
    public static BoxCollider2D boxCollider2D;
    public static CapsuleCollider2D capsuleCollider2D;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.3f;
    public float jumpForce = 1f;

    public Transform player;
    public float followRange = 0.7f;
    public float distToPlayer;
    public float knockbackX = 1f, knockbackY = 1f;
    bool inRange;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        herzIsAlive = true;
        herzCurrentHealth = herzMaxHealth;
        healthBar.SetMaxHealth(herzMaxHealth);
    }
    void FixedUpdate()
    {
        Follow();
    }
    private void LateUpdate()
    {
        FollowRange();
    }
    void Flip()
    {
        if (transform.position.x > player.position.x && m_FacingRight || transform.position.x < player.position.x && !m_FacingRight)
        {
            m_FacingRight = !m_FacingRight;
            transform.Rotate(0f, 180f, 0f);
            moveSpeed *= -1;
        }
    }
    void Follow()
    {
        if (!PlayerStatus.playerAlive)
            return;
        Flip();
        if (inRange)
            return;
        if (PlayerMovement.crouching)
            rb.velocity = new Vector2((moveSpeed * crouchSpeedMultiplier) * Time.fixedDeltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    void FollowRange()
    {
        if (!PlayerStatus.playerAlive)
            return;
        distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < followRange)
            inRange = true;
        else
            inRange = false;
    }
    public void TakeDamage(int damage, float direction)
    {
        herzCurrentHealth -= damage;
        healthBar.SetHealth(herzCurrentHealth);
        if (herzCurrentHealth <= 0)
        {
            Die();
            return;
        }

        if (direction >= 0f)
        {
            rb.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-knockbackX, knockbackY), ForceMode2D.Impulse);
        }
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        herzIsAlive = false;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        //if can collide with enemies, find a way to detect only ground and object layers
        //if (collision.IsTouchingLayers(groundLayer))
        //{
        //    print("jump");
        //    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        //}
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(ceilingCheck.position, ceilingCheckRadius);
    //}
}
