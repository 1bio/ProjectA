using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    public Button startHost;
    public Button startClient;
    public Button startServer;

    public Canvas ui;

    void Start()
    {
        startHost.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); ui.enabled = false; });
        startClient.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); ui.enabled = false; });
        startServer.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); ui.enabled = false; });
    }
}
