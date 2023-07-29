using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementRanged : EnemyMovement
{
    [SerializeField] protected WeaponRangedEnemy weaponRangedEnemy;
    [SerializeField] protected float keepDistanceFromHostile;
    [SerializeField] protected bool isKeepingDistanceFromHostile;
    protected string keepingDistanceState = "Keeping Distance";
    protected string aggroedAndReloadingState = "Aggroed and Reloading";

    public override void Awake()
    {
        base.Awake();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public void Update()
    {   
        //if (m_FacingRight)
        //    Debug.Log("FACING RIGHT");
        //else
        //    Debug.Log("FACING LEFT");
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
        if (isAggroed)
        {
            patroling = false;
            if (hostileTransformCPC2D[0] == null)
            {
                Debug.Log("KEEPING DISTANCE FROM NULL");
            }
            else
            {
                KeepDistanceFromHostile(hostileTransformCPC2D[0]);

            }
            if (weaponRangedEnemy.isReloading && isAggroed && !isKeepingDistanceFromHostile)
            {
                characterStateManager.ChangeState(aggroedAndReloadingState);
                return;
            }

            if (!isKeepingDistanceFromHostile && !weaponRangedEnemy.isReloading)
            {
                willAttack = true;
                return;
            }
            //else if (isAggroed)
            //{
            //    characterStateManager.ChangeState(aggroedState);
            //    return;
            //}
        }
        else
        {
            willAttack = false;
            patroling = true;
            //weaponRangedEnemy.isShooting = false;

             if (weaponRangedEnemy.isReloading && patroling)
            {
                characterStateManager.ChangeState(weaponRangedEnemy.runningAndReloadingState);
                 return;
             }
             else if (patroling)
            {
                characterStateManager.ChangeStateWithAnimation(runningState);
                 return;
             }
        }

        

    }
    void KeepDistanceFromHostile(CapsuleCollider2D hostileTransformCPC2D)//parameter unused for now
    {
        if (distToHostile <= keepDistanceFromHostile)
        {
            isKeepingDistanceFromHostile = true;
            Flip();
            //Debug.Log("FLIPED IN EnemyMovementRanged KEEPING DISTANCE");
            //rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            patroling = true;

            if (weaponRangedEnemy.isReloading && isKeepingDistanceFromHostile)
            {
                characterStateManager.ChangeState(weaponRangedEnemy.keepingDistanceAndReloadingState);
                return;
            }
            else if (isKeepingDistanceFromHostile)
            {
                characterStateManager.ChangeState(keepingDistanceState);
                return;
            }
        }
        else
        {
            isKeepingDistanceFromHostile = false;
            patroling = false;
        }
    }
    
}


//        else if (isAggroed)
//        {
//              characterStateManager.ChangeState(aggroedState);
//              return;
//        }