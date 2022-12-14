
using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.Anim.SetBool("Aiming", false);
        aim.CurrentFov = aim.HipFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
