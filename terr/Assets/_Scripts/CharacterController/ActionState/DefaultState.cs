using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager act) { }
    public override void UpdateState(ActionStateManager act)
    {
        if (Input.GetKeyDown(KeyCode.R) && CanRealod(act))
        {
            act.SwitchState(act.Reaload);
        }
    }

    bool CanRealod(ActionStateManager act)
    {
        if (!act.ReloadIsOver) return false;
        if (act.Ammo.CurrentAmmo == act.Ammo.ClipSize) return false;
        else if (act.Ammo.ExtraAmmo == 0) return false;
        else return true;
    }
}
