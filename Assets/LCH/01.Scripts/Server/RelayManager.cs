using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;
using QuickCmd;

public class RelayManager : MonoBehaviour
{
    public Canvas ui;

    [Command]
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

    [Command]
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
                ui.enabled = true;
            }
            else
            {
                // 호스트 실행
                NetworkManager.Singleton.StartHost();
                Debug.Log($"Started Relay Server With {joinCode}");
                ui.enabled = true;
            }

            return (true, joinCode);
        }
        catch (Exception e)
        {
            Debug.Log($"Create Relay Failed. {e}");
            return (false, string.Empty);
        }
    }

    [Command]
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
            ui.enabled = true;
            return true;
        }
        catch (Exception e)
        {
            //Debug.Log($"JoinRelay Failed. {e}");
            return false;
        }
    }
}
