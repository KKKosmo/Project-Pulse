using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangedPlayerPrefab : WeaponRanged
{
    Vector3 crouchFirePoint;
    [SerializeField] float crouchFirePointHeight = -0.129f;
    public static float range = 1; //transfer this to weapon when making ranged enemies aggro range and fire range

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }
    }
    void FixedUpdate()
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
        float randomZ;

        //maybe this doesnt work because of the whole interdependent thing
        //Instantiate(bulletPrefab, crouchFirePoint, Quaternion.Euler(0f, firePoint.rotation.y, randomZ));
        if (!PlayerMovement2.isCrouching)
        {
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));

            Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            return;
        }
        else
        {
            //simpler if i can add crouchFirePointHeight to firePoint.position.y
            //firePoint.position.y += crouchFirePointHeight;
            //"should not change the quaternion interdependently, use a quaternion function instead"
            //would be faster if i can just set firepoint rotation directly rather than combining two quaternions but i need to combine because of left/right AKA y
            crouchFirePoint.y = attackPoint.position.y + crouchFirePointHeight;
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, crouchFirePoint, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, crouchFirePoint, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));

            Instantiate(bulletPrefab, crouchFirePoint, attackPoint.rotation);
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, crouchFirePoint, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, crouchFirePoint, attackPoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload(reloadTime));
        }
    }
}