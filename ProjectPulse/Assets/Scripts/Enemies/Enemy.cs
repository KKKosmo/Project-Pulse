using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	[SerializeField] int maxHealth = 100;
	[SerializeField] int currentHealth;
	public GameObject deathEffect;//just do animation instead of prefab.

	public Rigidbody2D rb;
	private bool patroling;
	public float walkSpeed;
	private bool m_FacingRight = true;

	public Transform groundCheck;
	bool mustFlip;
	public LayerMask groundLayer;
	public float groundCheckRadius = 0.3f;

	public Collider2D wallCheckBodyCollider;
	public Transform firePoint;
	public Transform player, herz;

	public static float aggroRange = 2;
	private float distToPlayer;
	private float distToHerz;

	public float attackCooldown = 1;
	public GameObject enemyBulletPrefab;
	//enable shooting bool for animation
	//private bool shooting;
	//enable canshoot together with aggro and Ienum
    //private bool canShoot = true;

    public void Awake()
	{
		currentHealth = maxHealth;
		player = GameObject.Find("Player").transform;
		herz = GameObject.Find("Herz").transform;
		patroling = true;
		//enemyBulletPrefab = (GameObject)Resources.Load("Assets/Prefabs/EnemyBullet");
	}
	private void FixedUpdate()
	{
		if (patroling)
		{
			Patrol();
		}
	}
	private void LateUpdate()
	{
		if (patroling)
		{
			mustFlip = !Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		}
		//Aggro();
	}
	void Patrol()
	{
		if (mustFlip || wallCheckBodyCollider.IsTouchingLayers(groundLayer))//ground infront of him
		{
			Flip();
		}
		rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
	}
	void Flip()
	{
		patroling = false;
		m_FacingRight = !m_FacingRight;
		transform.Rotate(0f, 180f, 0f);
		walkSpeed *= -1;
		patroling = true;
	}
    void Aggro()
    {
        //if (PlayerStatus2.playerAlive)//or just cut to cutscene when die so no error
        //{
        //    distToPlayer = Vector2.Distance(transform.position, player.position);
        //    if (distToPlayer <= aggroRange)
        //    {
        //        if (player.position.x > transform.position.x && !m_FacingRight || player.position.x < transform.position.x && m_FacingRight)
        //        {
        //            Flip();
        //        }
        //        patroling = false;
        //        if (canShoot)
        //            StartCoroutine(Shoot());
        //    }
        //    else
        //    {
        //        patroling = true;
        //    }
        //}
        //if (Herz.herzIsAlive)//or just cut to cutscene when die so no error
        //{
        //    distToHerz = Vector2.Distance(transform.position, herz.position);
        //    if (distToHerz <= aggroRange)
        //    {
        //        if (herz.position.x > transform.position.x && !m_FacingRight
        //            || herz.position.x < transform.position.x && m_FacingRight)
        //        {
        //            Flip();
        //        }
        //        patroling = false;
        //        if (canShoot)
        //            StartCoroutine(Shoot());
        //    }
        //    else
        //    {
        //        patroling = true;
        //    }
        //}
    }
    public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			Die();
		}
	}
	void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	//IEnumerator Shoot()
	//{
 //       canShoot = false;
 //       //shooting = true;
 //       yield return new WaitForSeconds(attackCooldown);
 //       Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
 //       canShoot = true;
	//}
	//private void OnDrawGizmosSelected()
	//{
	//    Gizmos.color = Color.yellow;
	//    Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
	//    //Gizmos.color = Color.red;
	//    //Gizmos.DrawSphere(ceilingCheck.position, ceilingCheckRadius);
	//}
}//class
