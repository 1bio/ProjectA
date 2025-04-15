using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State state;

    public void ChangeState(State newState)
    {
        state?.Exit();
        state = newState;
        state?.Enter();
    }

    private void Update()
    {
        state?.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        state?.PhysicsUpdate(Time.fixedDeltaTime);
    }
}
