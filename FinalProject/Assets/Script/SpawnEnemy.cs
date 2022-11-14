using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemy;
    [SerializeField] private GameObject _zone;
    [SerializeField] private int _countEnemy;

    private Enemy _enemyClass;

    private LayerMask _mask;
    
    private bool _isSpawned = false;
    private void Start()
    {
        _mask = LayerMask.GetMask("Road");
    }
    private void Update()
    {
        if(_isSpawned == false)
        {
            StartCoroutine(timeDelay());
        }
    }
    private IEnumerator timeDelay()
    {
        _isSpawned = true;
        Spawn();
        yield return new WaitForSeconds(30);
        _isSpawned = false;
    }
    private void Spawn()
    {
        Collider[] _overlapBoxs = Physics.OverlapBox(gameObject.transform.position, _zone.transform.localScale / 2, Quaternion.identity,_mask) ;
        for (int i = 0; i < _countEnemy; i++)
        {
            Vector3 _position = _overlapBoxs[Random.Range(0, _overlapBoxs.Length)].transform.position;
            Enemy _enemyCopy = Instantiate(_enemy[Random.Range(0, _enemy.Length)],new Vector3(_position.x, _position.y, _position.z), Quaternion.identity) as Enemy;   
            _enemyCopy._target = gameObject.transform;
        }

    }
}
