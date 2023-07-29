using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRanged : CharacterWeapon
{
    [SerializeField] protected int maxAmmo = 2;
    public int currentAmmo;
    public float reloadTime = 1f;
    public bool isReloading;
    public float bulletSpeed;

    [HideInInspector] public string reloadingState = "Reloading";
    [HideInInspector] public string runningAndReloadingState = "Running and Reloading";
    [HideInInspector] public string keepingDistanceAndReloadingState = "Keeping Distance and Reloading";

    [SerializeField] protected GameObject bulletPrefab;

    public override void Awake()
    {
        base.Awake();
        isReloading = false;
        currentAmmo = maxAmmo;
    }
    public IEnumerator Reload(float reloadTime)
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        //Debug.Log(gameObject.name + " IS DONE RELOADING");
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}