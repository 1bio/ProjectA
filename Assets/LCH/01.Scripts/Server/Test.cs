using Unity.Netcode;
using UnityEngine;

public class Test : NetworkBehaviour
{
    void Update()
    {
        // �������� �ڽ��� �ƴϸ�
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
