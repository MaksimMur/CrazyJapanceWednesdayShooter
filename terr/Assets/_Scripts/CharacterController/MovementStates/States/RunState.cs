using UnityEngine;
public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.SetAudioRunning(true);
        movement.Anim.SetBool("Running", true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Walk);
        else if (movement.Dir.magnitude < 0.1f) ExitState(movement, movement.Idle);

        if (movement.VInput < 0) movement.CurrentMoveSpeed = movement.RunBackSpeed;
        else movement.CurrentMoveSpeed = movement.RunSpeed;
    }

    private void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.SetAudioRunning(false);
        movement.Anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
