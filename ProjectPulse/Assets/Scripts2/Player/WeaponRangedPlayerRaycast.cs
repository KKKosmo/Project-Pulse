using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponRangedPlayerRaycast : WeaponRanged
{
    Vector3 crouchFirePoint;
    public float crouchFirePointHeight = -0.129f;
    public float range = 1;
    public GameObject impactEffect;
    public float spreadArea = 0.1f;
    public LayerMask layer_mask;
    public float lineDebugDuration;
    


    public override void Awake()
    {
        base.Awake();
        layer_mask = LayerMask.GetMask("Ground", "Enemy");
    }
    private void FixedUpdate()
    {
        if (PlayerMovement2.isCrouching)
            crouchFirePoint = attackPoint.position;
    }
    


    void Shoot()
    {
        nextTimeToFire = Time.time + 1f / attackSpeed;
        if (isReloading)
            return;
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload(reloadTime));
            return;
        }
        currentAmmo--;
        if (!PlayerMovement2.isCrouching)
        {
            StandingShoot();
        }
        else
        {
            CrouchingShoot();
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload(reloadTime));
        }
    }
    void StandingShoot()
    {
        for (int i = 0; i <= 5; i++)
        {
            float random = Random.Range(-spreadArea, spreadArea);
            Vector3 spread = attackPoint.up * random;
            Vector3 direction = attackPoint.right + spread;
            RaycastHit2D raycastHit = Physics2D.Raycast(attackPoint.position, direction, range, layer_mask);
            if (raycastHit)
            {
                CharacterStatus character = raycastHit.transform.GetComponent<CharacterStatus>();
                if (character != null)
                {
                    CharacterMovement characterMovement = raycastHit.transform.GetComponent<CharacterMovement>();
                    character.TakeDamage(raycastHit.rigidbody, damage, transform.rotation.y, knockback, knockback, staggerDuration, characterMovement);
                }
                Instantiate(impactEffect, raycastHit.point, Quaternion.identity);
                Debug.DrawLine(attackPoint.position, raycastHit.point, Color.green, lineDebugDuration);
            }
            else
            {
                Debug.DrawLine(attackPoint.position, attackPoint.position + direction * range, Color.red, lineDebugDuration);
            }
        }
    }
    void CrouchingShoot()
    {
        for (int i = 0; i <= 5; i++)
        {
            crouchFirePoint.y = attackPoint.position.y + crouchFirePointHeight;
            float random = Random.Range(-spreadArea, spreadArea);
            Vector3 spread = attackPoint.up * random;
            Vector3 direction = attackPoint.right + spread;
            RaycastHit2D raycastHit = Physics2D.Raycast(crouchFirePoint, direction, range, layer_mask);
            if (raycastHit)
            {
                CharacterStatus character = raycastHit.transform.GetComponent<CharacterStatus>();
                if (character != null)
                {
                    //character.TakeDamage(damage);
                }
                Instantiate(impactEffect, raycastHit.point, Quaternion.identity);
                Debug.DrawLine(crouchFirePoint, raycastHit.point, Color.green, lineDebugDuration);
            }
            else
            {
                Debug.DrawLine(crouchFirePoint, crouchFirePoint + direction * range, Color.red, lineDebugDuration);
            }
        }
        //if (currentAmmo <= 0)
        //{
        //    StartCoroutine(Reload(reloadTime));
        //}

    }
}
