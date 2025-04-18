using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Move()
    {

        if (stateMachine.InputReader.isMoving)
        {
            Vector3 direction = new Vector3(stateMachine.InputReader.MoveValue.x, 0, stateMachine.InputReader.MoveValue.y);
            Rotate(direction);
            stateMachine.Rigidbody.linearDamping = stateMachine.PlayerGroundData.groundDrag;
            stateMachine.Rigidbody.AddForce(direction * stateMachine.PlayerGroundData.moveSpeed, ForceMode.VelocityChange);
        }
        else
        {
            stateMachine.Rigidbody.AddForce(Vector3.zero);
        }
    }

    public void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        stateMachine.transform.rotation = Quaternion.Slerp(
            stateMachine.transform.rotation, targetRotation, stateMachine.RotateSpeed * Time.fixedDeltaTime
            );
    }
}
