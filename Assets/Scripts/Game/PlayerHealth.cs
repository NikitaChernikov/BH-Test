using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [Header("Reference")]
    [SerializeField] private SkinnedMeshRenderer _playerMesh;
    [SerializeField] private Material _damagedMaterial;
    [SerializeField] private Image _healthBar;

    [Header("Settings")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private int _damageToTake = 1;
    [SerializeField] private float _colorChangingTime = 3.0f;

    [SyncVar] private int _health;
    private bool _isInvincible = false;
    private Material _normalMaterial;

    private void Start()
    {
        _normalMaterial = _playerMesh.material;
        _health = _maxHealth;
    }

    public int GetHealth()
    {
        return _health;
    }

    [ClientRpc]
    public void RpcTakeDamage()
    {
        if (!_isInvincible)
        {
            CmdMakeInvincible();
            _health -= _damageToTake;
            CmdChangeHealthBar();
            if (_health <= 0)
            {
                RpcHandleDeath();
                return;
            }
        }
    }

    [Command]
    private void CmdChangeHealthBar()
    {
        RpcChangeHealthBar();
    }

    [ClientRpc]
    private void RpcChangeHealthBar()
    {
        _healthBar.fillAmount = (float)_health / _maxHealth;
    }


    [Command]
    private void CmdMakeInvincible()
    {
        _isInvincible = true;
        RpcChangeColor();
    }

    [ClientRpc]
    private void RpcChangeColor()
    {
        _playerMesh.material = _damagedMaterial;
        Invoke("MakeNormal", _colorChangingTime);
    }

    public void MakeNormal()
    {
        _isInvincible = false;
        _playerMesh.material = _normalMaterial;
    }

    [ClientRpc]
    private void RpcHandleDeath()
    {
        gameObject.SetActive(false);
    }
}
