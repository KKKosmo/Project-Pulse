using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveForce = 1.5f;
    public float jumpForce = 3f;
    public float crouchSpeedMultiplier = 0.5f;
    float movementX;
    bool m_FacingRight = true;

    [SerializeField]
    public static Rigidbody2D playerBody;
    public static BoxCollider2D boxCollider2D;
    public static CapsuleCollider2D capsuleCollider2D;
    bool isGrounded = false;
    public Transform groundCheck;
    public float groundCheckRadius = 0.05f;
    public Transform ceilingCheck;
    public float ceilingCheckRadius = 0.05f;
    public static bool crouching;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    bool jumping;

    public static Animator anim;
    readonly string runAnimation = "Run";
    readonly string crouchAnimation = "Crouch";
    readonly string jumpAnimation = "Jump";
    readonly string shootAnimation = "Shoot";

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent <CapsuleCollider2D>();
    }
    void FixedUpdate()
    {
        Grounded();
        PlayerCrouch();
        PlayerMoveKeyboard();
        PlayerJump();
    }
    void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal");
    }
    private void LateUpdate()
    {
        AnimatePlayer();
    }
    void Flip()
    {
        //Will make 2 flip functions if bugs occur, one for animation in lateupdate and one for physics in fixedupdate
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    void PlayerMoveKeyboard()
    {
        if (!crouching)
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
    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            anim.SetBool(runAnimation, true);
        }
        else if (movementX < 0)
        {
            anim.SetBool(runAnimation, true);
        }
        else
        {
            anim.SetBool(runAnimation, false);
        }
        if (crouching)
        {
            anim.SetBool(crouchAnimation, true);
        }
        else
        {
            anim.SetBool(crouchAnimation, false);
        }
        if (jumping)
        {
            anim.SetBool(jumpAnimation, true);
        }
        else
        {
            anim.SetBool(jumpAnimation, false);
        }
        if (Input.GetButton("Fire1"))
        {
            anim.SetBool(shootAnimation, true);
        }
        else
        {
            anim.SetBool(shootAnimation, false);
        }
    }
    void Grounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))// || Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, enemyLayer) if can collide with enemies
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void PlayerJump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            playerBody.velocity = Vector3.zero;//see to this if bugging when getting hit while jumping
            playerBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumping = true;
        }
        else if (isGrounded)
        {
            jumping = false;
        }
    }
    void PlayerCrouch()
    {
        if (Input.GetButton("Crouch")){
            crouching = true;
            boxCollider2D.enabled = false;
        }
        else
        {
            if (!Input.GetButton("Crouch") && !Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayer))
            {
                boxCollider2D.enabled = true;
                crouching = false;
            }
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
