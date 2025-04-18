using Unity.Netcode;
using UnityEngine;

public abstract class State : NetworkBehaviour
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void PhysicsUpdate(float fixedDeltaTime);
    public abstract void Exit();
}
