using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public InputReader InputReader { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }


    [field: SerializeField] public float MoveSpeed { get; set; } = 5f;

    private void Awake()
    {
        InputReader = GetComponent<InputReader>();

        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }

    private void Start()
    {
        ChangeState(new PlayerIdlingState(this));
    }
}
