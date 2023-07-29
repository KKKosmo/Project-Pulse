using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerzMovement : AIMovement
{
    [SerializeField] float crouchSpeedMultiplier = 0.65f;
    private bool isInRange;
    [SerializeField] Transform wallCheck;
    [SerializeField] float wallCheckRadius;
    [SerializeField] float followRange;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (characterStatus.canMove)
        {
            Follow();
        }
        
    }
    public override void LateUpdate()
    {
        if (characterStatus.canMove)
        {
            base.LateUpdate();
            Range();
        }
        
    }
    void Range()
    {
        if (player == null)
            return;
        distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < followRange)
            isInRange = true;
        else
            isInRange = false;
    }
    void Follow()
    {
        
        if (player == null)
            return;
        if (transform.position.x > player.position.x && m_FacingRight || transform.position.x < player.position.x && !m_FacingRight)
        {
            Flip();
        }
        if (isInRange)
        {
            characterStateManager.ChangeState(idleState);
            return;
        }
        else
        {
            if (isGrounded)
                characterStateManager.ChangeState(runningState);
        }
        if (Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, groundLayer) && isGrounded)
        {
            characterStateManager.ChangeState(jumpingState);
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        if (PlayerMovement.crouching)
            rb.velocity = new Vector2((moveSpeed * crouchSpeedMultiplier) * Time.fixedDeltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(ceilingCheck.position, ceilingCheckRadius);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground"))
    //    {
    //        herz.rb.AddForce(new Vector2(0f, herz.jumpForce), ForceMode2D.Impulse);
    //    }
    //}
}
