using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : CharacterMovement
{
    [SerializeField] float jumpForce = 2.4f;
    [SerializeField] float moveForce = 1.5f;
    [SerializeField] float crouchSpeedMultiplier = 0.5f;
    float movementX;

    [SerializeField] Transform ceilingCheck;
    [SerializeField] float ceilingCheckRadius = 0.09f;
    public static bool isCrouching;

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] WeaponRangedPlayerRaycast weaponRangedPlayerRaycast;

    float jumpTimeCounter;
    [SerializeField] float jumpTime;
    [SerializeField] bool isJumping;

    Vector2 StandingCPC2D_Size;
    Vector2 CrouchingCPC2D_Size;
    Vector2 StandingCPC2D_Offset;
    Vector2 CrouchingCPC2D_Offset;

    string crouchingState = "Crouching";
    public override void Awake()
    {
        base.Awake();
        StandingCPC2D_Offset = new Vector2(0.0360202789f, -0.0542248189f);
        StandingCPC2D_Size = new Vector2(0.149597168f, 0.371785343f);
        CrouchingCPC2D_Offset = new Vector2(0.0360202789f, -0.120673627f);
        CrouchingCPC2D_Size = new Vector2(0.149597168f, 0.238887727f);
    }
    void FixedUpdate()
    {
        if (characterStatus.canMove)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            Move();
            PlayerCrouch();
            if (isJumping)
                PlayerJump();
        }
    }
    void Update()
    {
        if (characterStatus.canMove)
        {
            movementX = Input.GetAxisRaw("Horizontal");
            if (Input.GetButton("Jump") && isGrounded)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
            if (Input.GetButton("Crouch"))
            {
                isCrouching = true;
            }
            else if (!Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayer))
            {
                isCrouching = false;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                characterStateManager.ChangeStateWithAnimation(weaponRangedPlayerRaycast.lightAttackState);
                return;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                characterStateManager.ChangeStateWithAnimation(weaponRangedPlayerRaycast.heavyAttackState);
                return;
            }
            if (isGrounded)
            {
                if (isCrouching)
                {
                    characterStateManager.ChangeStateWithAnimation(crouchingState);
                    return;

                }
                if (movementX == 0f)
                {

                    characterStateManager.ChangeStateWithAnimation(idleState);
                    return;
                }
                else
                {
                    characterStateManager.ChangeStateWithAnimation(runningState);
                    return;
                }
            }
            else
            {
                    //if (!isCrouching)
                characterStateManager.ChangeStateWithAnimation(jumpingState);
            }
        }
    }
    void Move()
    {
        if (!isCrouching)
        {
            transform.position += (new Vector3(movementX, 0f, 0f) * moveForce) * Time.deltaTime;
        }
        else
        {
            transform.position += (new Vector3(movementX, 0f, 0f) * (moveForce * crouchSpeedMultiplier)) * Time.deltaTime;
        }
        if (movementX > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (movementX < 0 && m_FacingRight)
        {
            Flip();
        }
    }
    
    void PlayerJump()
    {
        rb.velocity = Vector2.up * jumpForce;
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                isGrounded = false;
                //characterStateManager.ChangeStateWithAnimation(jumpingState);//separated because if mc is falling without jumping, we dont want to play jump animation, play a different animation like fall animation or something
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

    }
    void PlayerCrouch()
    {
        if (isCrouching)
        {
            //isCrouching = true;
            CPC2D.size = CrouchingCPC2D_Size;
            CPC2D.offset = CrouchingCPC2D_Offset;
        }
        else
        {
            //isCrouching = false;
            CPC2D.size = StandingCPC2D_Size;
            CPC2D.offset = StandingCPC2D_Offset;
            
        }
    }
    //see ceiling and ground check gizmos
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(ceilingCheck.position, ceilingCheckRadius);
    //}
}//class