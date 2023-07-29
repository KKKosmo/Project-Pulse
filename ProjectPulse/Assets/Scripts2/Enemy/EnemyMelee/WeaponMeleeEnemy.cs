using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMeleeEnemy : WeaponMelee
{
    [SerializeField] protected LayerMask friendly;
    [SerializeField] protected bool hitPlayer, hitHerz;
    CharacterStatusWithHealthBar playerStatus;
    PlayerMovement2 playerMovement2;
    CharacterStatusWithHealthBar herzStatus;

    public void Start()
    {

        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatusWithHealthBar>();
        playerMovement2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();
        herzStatus = GameObject.FindGameObjectWithTag("Herz").GetComponent<CharacterStatusWithHealthBar>();
        Debug.Log("START");
    }
    public override void OGShoot()
    {
        base.OGShoot();
        if (canAttack)
            Swing();
    }
    public override void NormalAttack()
    {
        base.NormalAttack();
        if (canAttack)
            Swing();
    }

    protected void Swing()
    {
        hitPlayer = false;
        hitHerz = false;
        //preAttack = false;
        //duringAttack = false;
        //postAttack = true;
        Collider2D[] hitFriendly = Physics2D.OverlapCircleAll(attackPoint.position, attackRangeRadius, friendly);
        foreach (Collider2D collider in hitFriendly)
        {
            if (collider.CompareTag("Player"))
            {
                if (hitPlayer)
                    return;
                //CharacterStatus character = collider.GetComponent<CharacterStatus>();
                if (playerStatus != null)
                {
                    hitPlayer = true;
                    playerStatus.TakeDamage(collider.attachedRigidbody, damage, transform.rotation.y, 1f, 1f, playerStatus.healthBar, staggerDuration, playerMovement2);
                }
            }
            //make a script for herz status
            if (collider.CompareTag("Herz"))
            {
                if (hitHerz)
                    return;
                hitHerz = true;
                //CharacterStatus character = collider.GetComponent<CharacterStatus>();
                if (herzStatus != null)
                {
                    herzStatus.TakeDamage(collider.attachedRigidbody, damage, transform.rotation.y, 1f, 1f, herzStatus.healthBar);
                }
            }
        }
    }
}