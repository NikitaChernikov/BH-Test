using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System.Linq;

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private static List<Transform> _spawnPoints = new List<Transform>();

    private int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        _spawnPoints.Add(transform);

        _spawnPoints = _spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    public static void RemoveSpawnPoint(Transform transform) => _spawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnPlayer;

    [ServerCallback]

    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = _spawnPoints.ElementAtOrDefault(nextIndex);

        if (spawnPoint == null)
        {
            return;
        }

        GameObject playerInstance = Instantiate(_playerPrefab, _spawnPoints[nextIndex].position,
            _spawnPoints[nextIndex].rotation);

        NetworkServer.Spawn(playerInstance, conn);

        nextIndex++;
    }
}
