using UnityEngine;
using System;

public class PlayerGroundState : PlayerBaseState
{
    private readonly int GroundAnimationHash = Animator.StringToHash("PlayerGroundState");

    private readonly int Velocity = Animator.StringToHash("Velocity");

    private readonly float Duration = 0.1f;

    private readonly float dampTime = 0.1f;

    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(GroundAnimationHash, Duration);
    }

    public override void PhysicsUpdate(float fixedDeltaTime)
    {
        Move();
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MoveValue != Vector2.zero)
        {
            stateMachine.Animator.SetFloat(Velocity, 1f, dampTime, deltaTime);
        }

        stateMachine.Animator.SetFloat(Velocity, 0f, dampTime, deltaTime);
    }

    public override void Exit()
    {
    }
}
