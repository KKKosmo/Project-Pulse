using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangedEnemySniper : WeaponRangedEnemy
{

    public IEnumerator LightAttack()
    {
        characterStateManager.ChangeState(lightAttackState);
        characterStatus.canMove = false;
        characterStateManager.ChangeState("Preparing to shoot");

        yield return new WaitForSeconds(2f);
        characterStateManager.ChangeState("shot");
        Shoot();
        yield return new WaitForSeconds(2f);
        characterStateManager.ChangeState("whole attack done");
        characterStatus.canMove = true;
    }
}
