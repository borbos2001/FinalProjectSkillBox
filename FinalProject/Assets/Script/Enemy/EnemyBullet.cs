using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _timeToDeSpawn;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _hitEffects;
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
        if (collision.gameObject.tag == "Player")
        {
            _hitDetected = true;
            _hitEffects.SetActive(true);
            StartCoroutine(timeDelay());
            collision.gameObject.GetComponent<HealthPlayer>().CheackHealth(_damage);
        }
        else
        {
            DeSpawn();
        }    
    }
    private IEnumerator timeDelay()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        DeSpawn();
    }
}
