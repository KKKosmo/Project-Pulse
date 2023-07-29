using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] protected CharacterStateManager characterStateManager;

    [SerializeField] protected int maxHealth;
    public int currentHealth;
    [SerializeField] protected bool isAlive;
    public bool canMove;
    //public bool isAttacking;

    public delegate void Pulse();
    public event Pulse PulseActivate;
    [SerializeField] protected bool humanForm;

    public LayerMask hostileMask;


    string staggeredState = "Staggered";
    public virtual void Awake()
    {
        //isAttacking = false;
        canMove = true;
        humanForm = true;
        PulseActivate += PulseMutate;
        currentHealth = maxHealth;
        //Debug.Log(gameObject.name + " IS AWAKE");
    }
    public void Update()
    {
        if (Input.GetButtonDown("Pulse"))
        {
            //Debug.Log("PULSE PRESSED");
            if (PulseActivate != null)
                PulseActivate();
        }
    }
    public void CanNotMove()
    {
        canMove = false;
    }
    public void CanMove()
    {
        canMove = true;
    }
    //just damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
    }
    //damage with knockback
    public void TakeDamage(Rigidbody2D rb, int damage, float direction, float knockbackX, float knockbackY)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        rb.velocity = Vector3.zero;
        if (direction >= 0f)//should go right
            rb.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(-knockbackX, knockbackY), ForceMode2D.Impulse);
    }
    //damage with knockback with healthbar
    public void TakeDamage(Rigidbody2D rb, int damage, float direction, float knockbackX, float knockbackY, HealthBar healthBar)
    {
        rb.velocity = Vector3.zero;
        if (direction >= 0f)//should go right
            rb.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(-knockbackX, knockbackY), ForceMode2D.Impulse);

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            isAlive = false;
            Die();
        }

    }
    //damage with knockback with animation, final for characters with no healthbar
    public virtual void TakeDamage(Rigidbody2D rb, int damage, float direction, float knockbackX, float knockbackY, float staggerDuration, CharacterMovement characterMovement)
    {
        rb.velocity = Vector3.zero;
        if (direction >= 0f)//should go right
            rb.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(-knockbackX, knockbackY), ForceMode2D.Impulse);
        if (staggerDuration != 0f)
        {
            if (characterMovement.onGoingCoroutine != null)
            {
                //Debug.Log(playerMovement2.onGoingCoroutine);
                StopCoroutine(characterMovement.onGoingCoroutine);
                characterMovement.onGoingCoroutine = null;
            }
            characterMovement.onGoingCoroutine =  StartCoroutine(Stagger(staggerDuration));
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
            Die();
        }
    }
    //damage with knockback with healthbar with animation, used for mc
    public void TakeDamage(Rigidbody2D rb, int damage, float direction, float knockbackX, float knockbackY, HealthBar healthBar, float staggerDuration, CharacterMovement characterMovement)
    {
        rb.velocity = Vector3.zero;
        if (direction >= 0f)//should go right
            rb.AddForce(new Vector2(knockbackX, knockbackY), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(-knockbackX, knockbackY), ForceMode2D.Impulse);
        if (staggerDuration != 0f)
        {
            if (characterMovement.onGoingCoroutine != null)
            {
                //Debug.Log(playerMovement2.onGoingCoroutine);
                StopCoroutine(characterMovement.onGoingCoroutine);
                characterMovement.onGoingCoroutine = null;
            }
            characterMovement.onGoingCoroutine = StartCoroutine(Stagger(staggerDuration));
        }

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            isAlive = false;
            Die();
        }
    }

    protected IEnumerator Stagger(float staggerDuration)
    {
        canMove = false;
        characterStateManager.ChangeStateWithAnimationReset(staggeredState);
        //Debug.Log(gameObject.name + " IS STAGGERED");
        yield return new WaitForSeconds(staggerDuration);
        //Debug.Log(gameObject.name + " IS NOT STAGGERED ANYMORE");
        canMove = true;
    }

    protected void Die()
    {
        Destroy(gameObject);
        PulseActivate -= PulseMutate;

    }
    protected virtual void PulseMutate()
    {
        //Debug.Log(gameObject.name + "HAS TRANSFORMED");
        //humanForm = !humanForm;
        //Debug.Log(gameObject.name + "humanForm = " + humanForm);
    }
}