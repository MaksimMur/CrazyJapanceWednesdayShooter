using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Text tAmountAmmo;
    [SerializeField] private Image gunImage;

    public void SetImageGun(Sprite gunImage)
    {
        this.gunImage.sprite = gunImage;
    }

    public void SetAmountAmmo(int cur, int all)
    {
        tAmountAmmo.text = $"{cur}/{all}";
    }
    
}
