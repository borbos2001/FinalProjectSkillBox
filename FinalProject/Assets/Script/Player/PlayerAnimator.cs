
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;
    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    public void Run(Vector3 _movement)
    {
        if (Mathf.Abs(_movement.x) > 0.1f || Mathf.Abs(_movement.z) > 0.1f)
        {
            _playerAnimator.SetBool("run", true);
        }
        else
        {
            _playerAnimator.SetBool("run", false);
        }
    }

}
