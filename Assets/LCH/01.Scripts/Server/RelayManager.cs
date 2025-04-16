using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class RelayManager : MonoBehaviour
{
    public Button singIn_button;
    public Button createRelay_button;
    public Button joinRelay_button;
    public InputField joinRelay_inputfield;

    private void Start()
    {
        singIn_button.onClick.AddListener(() => SignIn());
        createRelay_button.onClick.AddListener(() => CreateRelay(false, 2));
        joinRelay_button.onClick.AddListener(() => JoinRelay(joinRelay_inputfield.text));
    }

    public async Awaitable<(bool sucess, string playerId)> SignIn()
    {
        try
        {
            await UnityServices.InitializeAsync();
            if(AuthenticationService.Instance.IsSignedIn == false)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            return (true, AuthenticationService.Instance.PlayerId);
        }
        catch(Exception e)
        {
            Debug.Log($"SignIn Failed. {e}");
            return (false, string.Empty);
        }
    }

    public async Awaitable<(bool success, string joinCode)> CreateRelay(bool isServer, int maxPlayers)
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );

            if (isServer)
            {
                // 서버 실행
                NetworkManager.Singleton.StartServer();
                Debug.Log($"Started Relay Server With {joinCode}");
            }
            else
            {
                // 호스트 실행
                NetworkManager.Singleton.StartHost();
                Debug.Log($"Started Relay Server With {joinCode}");
            }

            return (true, joinCode);
        }
        catch (Exception e)
        {
            Debug.Log($"Create Relay Failed. {e}");
            return (false, string.Empty);
        }
    }

    public async Awaitable<bool> JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
                );

            // 클라이언트 실행
            NetworkManager.Singleton.StartClient();
            Debug.Log($"Started Relay With {joinCode}");
            return true;
        }
        catch (Exception e)
        {
            //Debug.Log($"JoinRelay Failed. {e}");
            return false;
        }
    }
}
