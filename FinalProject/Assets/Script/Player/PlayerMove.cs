
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _speed = 2f;

    private PlayerAnimator _animator;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
    }
    public void Move(Vector3 _movement)
    {
        Vector3 _newPosition = new Vector3 (transform.position.x + _movement.x * _speed,  0  , transform.position.z + _movement.z * _speed);
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime );
        _animator.Run(_movement);
    }
    public void Rotation(Vector3 hitPoint)
    {
        hitPoint = new Vector3 (hitPoint.x,  transform.position.y, hitPoint.z +1.4f);
        Vector3 relativePos = hitPoint - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);
    }
}
