using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMeleeEnemyThief : WeaponMeleeEnemy
{
    public IEnumerator LightAttack()
    {
        characterStateManager.ChangeState(lightAttackState);
        characterStatus.canMove = false;
        Debug.Log("GETTING READY TO SWING");
        yield return new WaitForSeconds(1f);
        Swing();
        //Debug.Log("SWING RECOIL");
        yield return new WaitForSeconds(1f);
        //Debug.Log("ATTACK DONE");
        characterStatus.canMove = true;
    }
}
