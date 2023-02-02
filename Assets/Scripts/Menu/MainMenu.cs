using Mirror;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Lobby script holder")]
    [SerializeField] private NetworkManagerLobby _networkManager;

    [Header("UI")]
    [SerializeField] private GameObject _landingPagePanel;

    public void HostLobby()
    {
        _networkManager.StartHost();
        _landingPagePanel.SetActive(false);
    }
}
