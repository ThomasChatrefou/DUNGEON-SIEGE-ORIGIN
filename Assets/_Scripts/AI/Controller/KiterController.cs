using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KiterController : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform Target;
    private IBehaviorTree _behaviorTree;
    private float _speed = 3, _attackRange = 20, _attackSpeed = 2, _projectileSpeed = 5, _projectileLifeTime = 4;
   
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new StayAtRangeStrategy(Target, _agent, _attackRange, _speed, _attackSpeed, transform, _projectileSpeed,_projectileLifeTime);
    }

    private void Update()
    {
        _behaviorTree.Execute(transform);
    }
}
