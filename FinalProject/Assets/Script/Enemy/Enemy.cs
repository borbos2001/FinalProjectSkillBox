using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform _target;
    void Start()
    {    
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        _agent.destination = _target.position;
    }
}
