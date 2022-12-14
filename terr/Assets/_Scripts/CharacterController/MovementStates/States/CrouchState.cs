using UnityEngine;
public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.Anim.SetBool("Crouching", true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) ExitState(movement, movement.Run);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ExitState(movement, movement.Walk);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ExitState(movement, movement.Walk);
        }

        if (movement.VInput < 0) movement.CurrentMoveSpeed = movement.CrouchBackSpeed;
        else movement.CurrentMoveSpeed = movement.CrouchSpeed;
    }
    private void ExitState(MovementStateManager movement, MovementBaseState state) 
    { 
        movement.Anim.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}
