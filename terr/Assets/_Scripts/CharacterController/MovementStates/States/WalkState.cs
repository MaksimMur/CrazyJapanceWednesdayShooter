using UnityEngine;
public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.SetAudioWalking(true);
        movement.Anim.SetBool("Walking", true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.Run);
        else if (Input.GetKeyDown(KeyCode.LeftControl)) ExitState(movement, movement.Crouch);
        else if (movement.Dir.magnitude < 0.1f) ExitState(movement, movement.Idle);

        if (movement.VInput < 0) movement.CurrentMoveSpeed = movement.WalkBackSpeed;
        else movement.CurrentMoveSpeed = movement.WalkSpeed;
    }

    private void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.Anim.SetBool("Walking", false);
        movement.SetAudioWalking(false);
        movement.SwitchState(state);
    }
}
