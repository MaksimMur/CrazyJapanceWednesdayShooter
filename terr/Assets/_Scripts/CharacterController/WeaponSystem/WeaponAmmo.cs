using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [SerializeField] private int clipSize;
    [SerializeField] private int extraAmmo;
    [SerializeField] private WeaponUI weapon;
    [SerializeField] private int currentAmmo;

    public int ClipSize { get=> clipSize; set=> clipSize=value; }
    public int ExtraAmmo { get => extraAmmo; set => extraAmmo = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }

    private void Awake()
    {
        currentAmmo = clipSize;
    }

    public void ChangeAmountAmmoUI()
    {
        weapon.SetAmountAmmo(currentAmmo, extraAmmo);
    }

    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {
                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                extraAmmo = leftOverAmmo;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
        weapon.SetAmountAmmo(currentAmmo, extraAmmo);
    }
}
