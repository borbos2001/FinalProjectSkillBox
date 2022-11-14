using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    private bool _shoot = false;
    private NavMeshAgent _agent;
    private Vector3 _relativePos;

    [SerializeField] private Transform _pointToShoot;
    [SerializeField] private GameObject _bulletEnemy;
    [SerializeField] private float _power;
    [SerializeField] private int _countBullet;
    
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _enemy._target.position;
    }
    private void Update()
    {
        if (_agent.remainingDistance < 3 && _shoot == false)
        {
            _relativePos = _enemy._target.position -_pointToShoot.position ;

            for (int i = 0; i < _countBullet; i ++)
            {
                GameObject _currentBullet = Instantiate(_bulletEnemy, _pointToShoot.position, Quaternion.identity);
                _currentBullet.GetComponent<Rigidbody>().AddForce(_relativePos.normalized * _power, ForceMode.Impulse);
            }

            StartCoroutine(_timeDelay());
        }
    }
    private IEnumerator _timeDelay()
    {
        _shoot = true;
        yield return new WaitForSeconds(2);
        _shoot = false;
    }
}
