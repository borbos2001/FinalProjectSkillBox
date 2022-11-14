
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    void Update()
    {
        transform.position = _player.transform.position;
    }
}
