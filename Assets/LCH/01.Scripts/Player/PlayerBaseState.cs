using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Move(float fixedDeltaTime)
    {
        if (stateMachine.InputReader.isMoving)
        {
            Vector3 dir = new Vector3(stateMachine.InputReader.MoveValue.x, 0, stateMachine.InputReader.MoveValue.y);
            stateMachine.Rigidbody.AddForce(dir * stateMachine.MoveSpeed, ForceMode.VelocityChange);
        }
        else
        {
            stateMachine.Rigidbody.linearVelocity = Vector3.zero;
        }
    }
}
