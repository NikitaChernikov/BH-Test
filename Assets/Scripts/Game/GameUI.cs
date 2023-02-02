using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text[] _playerNames;

    private NetworkGamePlayerLobby[] _networkPlayer;

    private void Start()
    {
        _networkPlayer = FindObjectsOfType<NetworkGamePlayerLobby>();
        for (int i = 0; i < _networkPlayer.Length; i++)
        {
            _playerNames[i].text = _networkPlayer[i].GetDisplayName();
        }
    }
}
