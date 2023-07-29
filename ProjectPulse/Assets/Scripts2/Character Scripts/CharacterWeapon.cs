using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    [SerializeField] protected CharacterStateManager characterStateManager;
    [SerializeField] protected CharacterStatus characterStatus;

    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected  float attackSpeed;
    [SerializeField] protected  float nextTimeToFire = 0f;
    [SerializeField] protected  bool canAttack;
    public int damage;
    public float staggerDuration;
    public float knockback;


    [HideInInspector] public string lightAttackState = "Light Attack";
    [HideInInspector] public string heavyAttackState = "Heavy Attack";

    public virtual void Awake()
    {
        canAttack = true;
    }
    public virtual void NormalAttack()
    {
        OGShoot();
    }

    public virtual void OGShoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / attackSpeed;
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }
}