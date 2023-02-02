using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerAnimations _playerAnim;

    [Header("Setting")]
    [SerializeField] private float _dashPower = 10.0f;
    [SerializeField] private float _dashCooldown = 2.0f;
    [SerializeField] private float _dashTime = 1.0f;

    private Rigidbody _rigidbody;
    private float _dashTimer;
    [SerializeField] private bool _isDashing = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _dashTimer = _dashCooldown;
    }

    private void Update()
    {
        if (isClient)
        {
            _dashTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_dashTimer >= _dashCooldown)
                {
                    _dashTimer = 0;
                    CmdDash();
                    Invoke("CmdStopDash", _dashTime);
                }
            }
        }
    }

    [ClientRpc]
    private void RpcActivateDash()
    {
        _isDashing = true;
        _playerAnim.ActivateDashAnim();
        _rigidbody.AddForce(transform.forward * _dashPower, ForceMode.Impulse);
    }

    [ClientRpc]
    private void RpcStopDash()
    {
        _isDashing = false;
        _rigidbody.velocity = Vector3.zero;
    }

    [Command]
    private void CmdDash()
    {
        RpcActivateDash();
    }

    [Command]
    private void CmdStopDash()
    {
        RpcStopDash();
    }

    public bool IsDashing()
    {
        return _isDashing;
    }

}
