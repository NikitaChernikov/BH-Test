using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{
    [Header("Reference")]
    [SerializeField] private PlayerAnimations _animations;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed = 10.0f;
    [SerializeField] private float _turningSmoothTime = 0.1f;

    [System.NonSerialized] public Vector3 MoveDirection;

    private Rigidbody _rigidbody;
    private Transform _camera;
    private float _horizontalInput;
    private float _verticalInput;
    private float _rotationAngle;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rigidbody.isKinematic = false;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if (hasAuthority)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                _animations.ActivateRunAnim();
                _rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _rotationAngle,
                    ref _turnSmoothVelocity, _turningSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                MoveDirection = (Quaternion.Euler(0f, _rotationAngle, 0f) * Vector3.forward).normalized;
            }
            else
            {
                _animations.ActivateIdelAnim();
                MoveDirection = Vector3.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasAuthority)
        {
            _rigidbody.MovePosition(transform.position + MoveDirection * _movementSpeed * Time.fixedDeltaTime);
        } 
    }
}