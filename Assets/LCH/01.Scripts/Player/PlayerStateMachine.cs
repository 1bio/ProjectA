using Unity.Netcode.Components;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public InputReader InputReader { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }
    public Animator Animator { get; private set; }

    [field: SerializeField] public PlayerGroundData PlayerGroundData { get; set; }
    [field: SerializeField] public float RotateSpeed { get; set; } = 10f;

    public void Initialize()
    {
        InputReader = GetComponent<InputReader>();

        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Animator = GetComponent<Animator>();

        PlayerGroundData = new PlayerGroundData(LayerMask.GetMask("Default"), 1.0f, 7, true);
    }

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        ChangeState(new PlayerGroundState(this));
    }
}
