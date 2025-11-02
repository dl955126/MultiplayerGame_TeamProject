using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class NetworkHUD : NetworkBehaviour
{
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void RestartGameButton()
    {
        if(!IsOwner) return;
        RestartGameRpc();
    }

    [Rpc(SendTo.Server)]
    public void RestartGameRpc()
    {
        NetworkManager.Singleton.Shutdown();
        NewPlayerInputs.playerCount = 0;
        Destroy(NetworkManager.Singleton);
        SceneManager.LoadScene("EnemiesTest");
    }
}
