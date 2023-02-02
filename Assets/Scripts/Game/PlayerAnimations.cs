using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void ActivateRunAnim()
    {
        _anim.SetBool("Run", true);
    }

    public void ActivateIdelAnim()
    {
        _anim.SetBool("Run", false);
    }

    public void ActivateDashAnim()
    {
        _anim.SetTrigger("Dash");
    }
}
