using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementRangedSniper : EnemyMovementRanged
{
    [SerializeField] protected WeaponRangedEnemySniper weaponRangedEnemySniper;
    public override void FixedUpdate()
    {
        if (characterStatus.canMove)
        {
            base.FixedUpdate();
        }
    }
    public override void LateUpdate()
    {
        if (characterStatus.canMove)
        {
            base.LateUpdate();
            if (weaponRangedEnemySniper.isReloading)
                return;
            if (weaponRangedEnemySniper.currentAmmo <= 0)
            {
                onGoingCoroutine = StartCoroutine(weaponRangedEnemySniper.Reload(weaponRangedEnemySniper.reloadTime));
                //isShooting = false;
            }
            if (!isKeepingDistanceFromHostile && !weaponRangedEnemy.isReloading && willAttack)
            {
                //Debug.Log("SHOULD ATTACK HERE");
                onGoingCoroutine = StartCoroutine(weaponRangedEnemySniper.LightAttack());
            }
        }
    }
}
