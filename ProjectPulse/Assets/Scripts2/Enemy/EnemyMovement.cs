using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : AIMovement
{
    [SerializeField] protected bool patroling;
	[SerializeField] protected bool isAggroed;
	bool mustFlipGround;
	[SerializeField] protected bool willAttack;

	protected float distToHostile;
	public float aggroRange;

	protected string aggroedState = "Aggroed";

	public CircleCollider2D hostileDetectCollider;
	protected CapsuleCollider2D[] hostileTransformCPC2D = new CapsuleCollider2D[10];

	public override void Awake()
	{
		base.Awake();
		patroling = true;
		isAggroed = false;
		willAttack = false;
		hostileDetectCollider.radius = aggroRange;
	}
	public override void FixedUpdate()
	{
		base.FixedUpdate();
		Patrol();
	}
	public override void LateUpdate()
	{
		base.LateUpdate();
		//if (hostileTransform[0] == null)
		//{
		//	Debug.Log("AGGROED TO NULL");
		//}
		//else
		//{
		//	Aggro(hostileTransform[0]);
		//}
		Aggro(hostileTransformCPC2D[0]);

	}
	void Patrol()
	{
		if (patroling)
		{
			mustFlipGround = !isGrounded || ((steeringRays[1] && m_FacingRight) || (steeringRays[5] && !m_FacingRight));
			if (mustFlipGround)
			{
				//Debug.Log("FLIPED IN EnemyMovement GROUND");
				Flip();
			}
			rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
		}

	}
	void Aggro(CapsuleCollider2D hostileTransformCPC2D)
	{
		if (hostileTransformCPC2D == null)
        {
			//Debug.Log("HOSTILETRANSFORM == NULL");
			return;
		}
		distToHostile = Vector2.Distance(transform.position, hostileTransformCPC2D.transform.position);
		if (distToHostile <= aggroRange)
		{
			if (hostileTransformCPC2D.transform.position.x > transform.position.x && !m_FacingRight || hostileTransformCPC2D.transform.position.x < transform.position.x && m_FacingRight)
			{
				Flip();
				//Debug.Log("FLIPED IN EnemyMovement");
			}
		}
		else
		{
		}
    }
	public void DisplayHostileTransfromArray()
	{
		for (int i = hostileTransformCPC2D.Length; i > 0; i--)
		{
			if (hostileTransformCPC2D[i - 1] != null)
                Debug.Log("-----" + gameObject.name + "'s hostileTransformCPC2D ARRAY'S [" + (i - 1) + "] IS " + hostileTransformCPC2D[i - 1].gameObject.name + "-----");
			//Debug.Log("-----" + "[" + (i-1) + "] IS " + hostileTransform[i - 1].gameObject.name + " OF GAME OBJECT " + gameObject.name + "-----");
		}
	}
	public void OnTriggerStay2D(Collider2D collision)
	{
		//or BoxCollider2D collider = col as BoxCollider2D
		if (collision is CapsuleCollider2D)
		{
			//might cause problems because capsule colliders are what is in their feet
			if (characterStatus.hostileMask == (characterStatus.hostileMask | (1 << collision.gameObject.layer)))
			{

				//Debug.Log(gameObject.name + " HAS DETECTED " + collision.gameObject.name + ", ITS WITHIN THE HOSTILE MASK");
				for (int i = 0; i < hostileTransformCPC2D.Length; i++)
				{
					if (collision == hostileTransformCPC2D[i])
					{
						//Debug.Log("***********DUPLICATE DETECTED: " + collision + ", DUPLICATE NOT ADDED INTO THE ARRAY.************");
						return;
					}
				}
				for (int i = 0; i < hostileTransformCPC2D.Length; i++)
				{
					if (hostileTransformCPC2D[i] == null)
					{
						hostileTransformCPC2D[i] = collision.GetComponent<CapsuleCollider2D>();
						//Debug.Log("-----" + collision.gameObject.name + " IS NOW IN hostileTransformCPC2D ARRAY [" + (i) + "] OF THE GAMEOBJECT " + gameObject.name + "-----");
						//DisplayHostileTransfromArray();
						isAggroed = true;
						return;
					}
				}
			}
			//display objects that are not in the hostile layer mask
			//else
			//{
			//    Debug.Log(gameObject.name + " HAS DETECTED AN OBJECT THATS NOT WITHIN THE HOSTILES LAYERMASK: " + collision.gameObject.name + ", ITS LAYER IS " + (1 << collision.gameObject.layer));
			//}
		}
    }
	public void OnTriggerExit2D(Collider2D collision)
    {
		if (collision is CapsuleCollider2D)
		{
			if (characterStatus.hostileMask == (characterStatus.hostileMask | (1 << collision.gameObject.layer)))
			{
				for (int i = 0; i < hostileTransformCPC2D.Length; i++)
				{
					if (collision == hostileTransformCPC2D[i])
					{
						//Debug.Log(collision.gameObject.name + " HAS LEFT THE hostileTransformCPC2D ARRAY OF " + gameObject.name);
						for (i += 1; i < hostileTransformCPC2D.Length; i++)
						{
							hostileTransformCPC2D[i - 1] = hostileTransformCPC2D[i];
						}
						//DisplayHostileTransfromArray();
						if (hostileTransformCPC2D[0] == null)
						{
							//Debug.Log("NO MORE ENEMIES IN RANGE OF THE GAME OBJECT " + gameObject.name + ", IT IS NOT AGGROED ANYMORE");
							isAggroed = false;
						}
						return;
					}
				}
			}
		}
	}

    //public virtual void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawSphere(ceilingCheck.position, ceilingCheckRadius);
    //}
}
