using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<HealthPlayer>().CheackHealth(_damage);
        }
    }
}
