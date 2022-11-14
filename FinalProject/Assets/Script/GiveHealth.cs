
using System.Collections;
using UnityEngine;

public class GiveHealth : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    [SerializeField] private GameObject _gameObject;    
    private AudioSource _audioSource;
    private Collider _collider;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthPlayer>().CheackHealth(-_health);
            _audioSource.Play();
            StartCoroutine(_timeToDestroy());
            
        }
    }
    private IEnumerator _timeToDestroy()
    {
        Destroy(_gameObject);
        _collider.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
