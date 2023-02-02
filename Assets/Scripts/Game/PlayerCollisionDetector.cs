using Mirror;
using UnityEngine;

public class PlayerCollisionDetector : NetworkBehaviour
{
    [Header("Reference")]
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerDash _playerDash;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.GetComponent<PlayerDash>().IsDashing());
            if (collision.gameObject.GetComponent<PlayerDash>().IsDashing())
            {
                CmdTakeDamage();
            }
            if (_playerDash.IsDashing())
            {
                CmdGiveDamage(collision.gameObject);
            }

        }
    }

    [Command(requiresAuthority = false)]
    private void CmdGiveDamage(GameObject enemy)
    {
        enemy.GetComponent<PlayerHealth>().RpcTakeDamage();
    }

    [Command]
    private void CmdTakeDamage()
    {
        _playerHealth.RpcTakeDamage();
    }

}
