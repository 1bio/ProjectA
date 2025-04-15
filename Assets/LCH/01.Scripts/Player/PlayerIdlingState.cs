using UnityEngine;
using System;

public class PlayerIdlingState : PlayerBaseState
{
    private PlayerStateMachine stateMachine;

    public PlayerIdlingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
    }

    public override void PhysicsUpdate(float fixedDeltaTime)
    {
        Move(fixedDeltaTime);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}
