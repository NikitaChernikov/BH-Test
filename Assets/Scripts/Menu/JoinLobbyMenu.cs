using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [Header("Lobby script holder")]
    [SerializeField] private NetworkManagerLobby _networkManager;

    [Header("UI")]
    [SerializeField] private GameObject _landingPagePanel;
    [SerializeField] private InputField _ipAdressInputField;
    [SerializeField] private Button _joinButton;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAdress = _ipAdressInputField.text;

        _networkManager.StartClient();
        _networkManager.networkAddress = ipAdress;


        _joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        _joinButton.interactable = true;

        gameObject.SetActive(false);
        _landingPagePanel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        _joinButton.interactable = true;
    }

}
