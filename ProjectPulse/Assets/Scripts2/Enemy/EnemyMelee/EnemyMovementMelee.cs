using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMelee : EnemyMovement
{
    [SerializeField] protected  WeaponMeleeEnemy weaponMeleeEnemy;

    public override void LateUpdate()
    {
        //if (weaponMeleeEnemy.postAttack)
        //{
        //    return;
        //}
        base.LateUpdate();
        if (isAggroed)
        {
            if (distToPlayer <= weaponMeleeEnemy.attackRangeRadius*2)
            {
                patroling = false;
                willAttack = true;
                //weaponMeleeEnemy.NormalAttack();
            }
            else
            {
                characterStateManager.ChangeState(aggroedState);
                willAttack = false;
                patroling = true;
            }
        }
        else
        {
            characterStateManager.ChangeState(runningState);
            willAttack = false;
            patroling = true;
        }
    }
    //public void OnDrawGizmosSelected()
    //{
    //    //base.OnDrawGizmosSelected();
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(weaponMeleeEnemy.attackPoint.position, weaponMeleeEnemy.attackRangeRadius);
    //}
}
