using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health ;

    [SerializeField] private GameObject[] _things;

    public void HealthControl(int damage)
    {
        _health = _health - damage;
        if (_health <= 0)
        {
            DeadEnemy();
        }
    }

    private void DeadEnemy()
    {
        int _randomChance = Random.Range(0, 12);
        if(_randomChance < 3)
        {
            Instantiate(_things[Random.Range(0, _things.Length)],gameObject.transform.position,Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
