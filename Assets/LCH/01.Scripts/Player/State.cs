using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void PhysicsUpdate(float fixedDeltaTime);
    public abstract void Exit();
}
