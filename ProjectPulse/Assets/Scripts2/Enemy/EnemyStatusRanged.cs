using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusRanged : EnemyStatus
{
    public WeaponRanged weaponRanged;
    public override void TakeDamage(Rigidbody2D rb, int damage, float direction, float knockbackX, float knockbackY, float staggerDuration, CharacterMovement characterMovement)
    {
        base.TakeDamage(rb, damage, direction, knockbackX, knockbackY, staggerDuration, characterMovement);
        if (weaponRanged.isReloading)
        {
            //Debug.Log("RELOAD CANCELLED");
            weaponRanged.isReloading = false;
        }
    }
}
