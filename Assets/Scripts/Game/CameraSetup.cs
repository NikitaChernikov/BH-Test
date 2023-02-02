using UnityEngine;
using Mirror;
using Cinemachine;

public class CameraSetup : NetworkBehaviour
{
    public CinemachineFreeLook Camera;
    [SerializeField] private Transform _lookAtObj;

    private void Start()
    {
        if (hasAuthority)
        {
            SetCamera();
        }
    }

    private void SetCamera()
    {
        Camera = FindObjectOfType<CinemachineFreeLook>();
        Camera.LookAt = _lookAtObj;
        Camera.Follow = _lookAtObj;
    }
}
