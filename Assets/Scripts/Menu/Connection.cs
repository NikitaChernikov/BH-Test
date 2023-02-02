using UnityEngine;
using Mirror;

public class Connection : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private NetworkManager _networkManager;

    [Header("Setting")]
    [SerializeField] private string _networkAddress = "localhost";

    private void Start()
    {
        if (!Application.isBatchMode)
        {
            _networkManager.StartClient();
        }
    }

    public void JoinClient()
    {
        _networkManager.networkAddress = _networkAddress;
        _networkManager.StartClient();
    }

}
