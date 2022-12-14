using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Shooting Options
    [Header("Fire Rate")]
    [SerializeField] private float fireRate;
    [SerializeField] private bool semiAuto;

    private float fireStart;
    #endregion

    #region Bullet Options
    [Header("Bullet Properties")]
    [SerializeField] private Transform bulletTransformAnchor;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrelPos;
    [SerializeField] private float bulletVelocity;
    [SerializeField] int bulletPerShot;

    private WeaponAmmo ammo;
    private AimStateManager aim;
    #endregion

    #region Audio
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private AudioClip reloadingClip;
    private AudioSource audioSource;
    #endregion

    #region UI

    WeaponUI weaponUI;

    #endregion
    private void Awake()
    {
        ammo = GetComponent<WeaponAmmo>();
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
    }

    public bool ShouldFire()
    {
        if (ammo.CurrentAmmo == 0) return false;
        if (fireStart + fireRate < Time.time)
        {
            if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0))
            {
                fireStart = Time.time;
                return true;
            }
            if (!semiAuto && Input.GetKey(KeyCode.Mouse0))
            {
                fireStart = Time.time;
                return true;
            }
        }
        return false;
    }

    public void Fire()
    {
        audioSource.PlayOneShot(shootingClip);
        barrelPos.LookAt(aim.AimPos);
        ammo.CurrentAmmo--;
        ammo.ChangeAmountAmmoUI();
        GameObject currentBullet = GameObject.Instantiate(bullet, barrelPos.position, barrelPos.rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        
    }
    public void PlayReloading()
    {
        audioSource.PlayOneShot(reloadingClip);
    }
}
