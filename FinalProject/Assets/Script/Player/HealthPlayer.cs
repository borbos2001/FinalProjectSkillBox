using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 30;

    [SerializeField] private Image _healthBar;

    private float _health;

    private bool _invulnerability = false;

    private Death _death;

    private void Start()
    {
        _health = _maxHealth;
        _death = GetComponent<Death>();
        _healthBar.fillAmount = _health / _maxHealth ;
    }
    public void CheackHealth(int _damage)
    {
        if (_invulnerability == false)
        {
            _health = _health - _damage;
            if(_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            _healthBar.fillAmount = _health / _maxHealth;
            if (_health <= 0)
            {
                _death.DeadPlayer();
            }
            StartCoroutine(InvulnerabilityTime());
        }
    }
    private IEnumerator InvulnerabilityTime()
    {
        _invulnerability = true;
        yield return new WaitForSeconds(1);
        _invulnerability = false;
    }
}
