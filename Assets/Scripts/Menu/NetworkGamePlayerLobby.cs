using Mirror;

public class NetworkGamePlayerLobby : NetworkBehaviour
{
    [SyncVar]
    private string _displayName = "Loading...";

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null)
            {
                return room;
            }
            return room = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        _displayName = displayName;
    }

    [Server]
    public string GetDisplayName()
    {
        return _displayName;
    }



}
