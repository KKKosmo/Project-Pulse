using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMeleeThief : EnemyMovementMelee
{
    [SerializeField] protected WeaponMeleeEnemyThief weaponMeleeEnemyThief;
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
            if (willAttack)
            {
                onGoingCoroutine = StartCoroutine(weaponMeleeEnemyThief.LightAttack());
            }
        }

    }
}
