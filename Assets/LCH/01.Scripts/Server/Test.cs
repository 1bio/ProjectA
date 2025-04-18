using Unity.Netcode;
using UnityEngine;

public class Test : NetworkBehaviour
{
    void Update()
    {
        // 소유권이 자신이 아니면
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            SayHelloRpc("Hello");
        }
    }

    [Rpc(SendTo.Everyone)]
    void SayHelloRpc(string message)
    {
        Debug.Log($"OwnerClientID:{OwnerClientId}, {message}");
    }
}
