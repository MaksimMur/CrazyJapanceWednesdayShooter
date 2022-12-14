using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealoadState : ActionBaseState
{
    public override void EnterState(ActionStateManager act)
    {
        act.ReloadIsOver = false;
        act.Anim.SetTrigger("Reload");
    }
    public override void UpdateState(ActionStateManager act)
    {

    }
}
