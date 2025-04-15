using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public PlayerInputs InputAction { get; private set; }

    public Vector2 MoveValue { get; private set; }

    public bool isMoving { get; private set; } = false;

    private void Awake()
    {
        InputAction = new PlayerInputs();
    }

    private void OnEnable()
    {
        InputAction.Enable();

        InputAction.Player.Move.performed += HandleMove;
        InputAction.Player.Move.canceled += HandleMove;
    }

    private void OnDisable()
    {
        InputAction.Player.Move.performed -= HandleMove;
        InputAction.Player.Move.canceled -= HandleMove;

        InputAction.Disable();
    }

    public void HandleMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isMoving = true;
            MoveValue = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            isMoving = false;
            MoveValue = Vector2.zero;
        }
    }
}
