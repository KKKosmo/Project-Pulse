using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected CharacterStateManager characterStateManager;
    [SerializeField] protected CharacterStatus characterStatus;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D CPC2D;
    [SerializeField] public bool m_FacingRight = true;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckRadius = 0.05f;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected float fallMultiplier = 1.5f;


    protected string idleState = "Idle";
    protected string runningState = "Running";
    protected string jumpingState = "Jumping";

    public Coroutine onGoingCoroutine;

    public virtual void Awake()
    {
        //characterStateManager = GetComponent<CharacterStateManager>();
    }
    public virtual void LateUpdate()
    {
        Grounded();
    }
    public virtual void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    void Grounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))// || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, enemyLayer) if can collide with enemies
        {
            isGrounded = true;
            //Debug.Log(gameObject.name + "IS GROUNDED");
        }
        else
        {
            isGrounded = false;
            //Debug.Log(gameObject.name + "IS NOT GROUNDED");
        }
    }
}//class
