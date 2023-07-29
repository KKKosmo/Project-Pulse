using System.Collections;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    Vector3 crouchFirePoint;
    public float crouchFirePointHeight = -0.129f;

    public float fireRate = 4;
    private float nextTimeToFire = 0f;

    public int maxAmmo = 2;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public GameObject bulletPrefab;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }
    private void FixedUpdate()
    {
        crouchFirePoint = firePoint.position;
    }
    void Update()
    {
        if (isReloading)
            return;
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        currentAmmo--;
        float randomZ;

        //maybe this doesnt work because of the whole interdependent thing
        //Instantiate(bulletPrefab, crouchFirePoint, Quaternion.Euler(0f, firePoint.rotation.y, randomZ));
        if (!PlayerMovement.crouching)
        {
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            return;
        }
        else
        {
            //simpler if i can add crouchFirePointHeight to firePoint.position.y
            //firePoint.position.y += crouchFirePointHeight;
            //"should not change the quaternion interdependently, use a quaternion function instead"
            //would be faster if i can just set firepoint rotation directly rather than combining two quaternions but i need to combine because of left/right AKA y
            crouchFirePoint.y = firePoint.position.y + crouchFirePointHeight;
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, crouchFirePoint, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, crouchFirePoint, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));

            Instantiate(bulletPrefab, crouchFirePoint, firePoint.rotation);
            randomZ = Random.Range(-5f, 5f);
            Instantiate(bulletPrefab, crouchFirePoint, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
            randomZ = Random.Range(-10f, 10f);
            Instantiate(bulletPrefab, crouchFirePoint, firePoint.rotation * Quaternion.Euler(0f, 0f, randomZ));
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    //if not rng bullets{
    //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation* Quaternion.Euler(0f, 0f, 5f));
    //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation* Quaternion.Euler(0f, 0f, -5f));
    //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation* Quaternion.Euler(0f, 0f, 10f));
    //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation* Quaternion.Euler(0f, 0f, -10f));
    //}
    
}//class