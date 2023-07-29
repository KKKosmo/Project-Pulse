using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangedEnemy : WeaponRanged
{
    public override void Awake()
    {
        base.Awake();
    }
    public override void NormalAttack()
    {
        if (isReloading)
            return;
        base.NormalAttack();
        if (!canAttack)
            return;
        //see if removing this reload if statement does anything
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload(reloadTime));
            //isShooting = false;
            return;
        }
        else
        {
            Shoot();
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload(reloadTime));
            //isShooting = false;
        }
    }
    public void Shoot()
    {
        //isShooting = true;
        currentAmmo--;
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        bullet.GetComponent<BulletEnemy2>().creator = gameObject;
    }
}