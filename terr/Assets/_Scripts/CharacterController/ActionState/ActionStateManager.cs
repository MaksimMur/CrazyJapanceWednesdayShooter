using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class ActionStateManager : MonoBehaviour
{
    #region Animation options 
    private ActionBaseState currentState;
    private Animator anim;
    public RealoadState Reaload { get; set; } = new RealoadState();
    public DefaultState Default { get; set; } = new DefaultState();
    public bool ReloadIsOver { get; set; } = true;
    public Animator Anim { get => anim; private set=> anim=value; }

    #endregion

    #region Weapon options
    [SerializeField] private WeaponManager currentWeapon;
    [SerializeField] private WeaponAmmo ammo;
    public WeaponManager CurrentWeapon { get=>currentWeapon; private set => currentWeapon = value; }
    public WeaponAmmo Ammo { get => ammo; private set => ammo = value; }
    #endregion

    private void Awake()
    {
        Cursor.visible = false;
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        SwitchState(Default);
        ammo.ChangeAmountAmmoUI();
    }

    private void Update()
    {
        if (currentWeapon.ShouldFire() && ReloadIsOver) currentWeapon.Fire();
        currentState.UpdateState(this);
    }
    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    public void ReloadWeapon()
    {
        ammo.Reload();
        SwitchState(Default);
    }
    public void ReloadOver() => ReloadIsOver = true;
    public void PlayReloadingSound() => currentWeapon.PlayReloading();
}
