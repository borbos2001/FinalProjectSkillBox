using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDeSpawn;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _hitEffects;
    [SerializeField] MeshRenderer _meshRenderer;
    private bool _hitDetected = false;
    private float _currentTime = 0;

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _timeToDeSpawn && _hitDetected != true)
        {
            DeSpawn();
        }
    }
    private void DeSpawn()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damaged")
        {
            _hitDetected = true; 
            _hitEffects.SetActive(true);
            StartCoroutine(_timeDelay());
            EnemyHealth _enemyHealt = collision.gameObject.GetComponent<EnemyHealth>();
            _enemyHealt.HealthControl(_damage);
        }
        else
        if (collision.gameObject.name != "Bullet(Clone)")
        {
            DeSpawn();
        }
    }
    private IEnumerator _timeDelay()
    {
        _meshRenderer.enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        DeSpawn();
    }
}
