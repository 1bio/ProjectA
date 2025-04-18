using UnityEngine;

[System.Serializable]
public class PlayerGroundData
{
    public LayerMask groundLayer;

    public float moveSpeed;
    public float groundDrag;

    public bool isGrounded;

    public PlayerGroundData(LayerMask groundLayer, float moveSpeed, float groundDrag, bool isGrounded)
    {
        this.groundLayer = groundLayer;
        this.moveSpeed = moveSpeed; 
        this.groundDrag = groundDrag;
        this.isGrounded = isGrounded;
    }
}
