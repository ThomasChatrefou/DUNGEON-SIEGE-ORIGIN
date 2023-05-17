using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KiterController : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform Target;
    private IBehaviorTree _behaviorTree;
    [SerializeField]
    private float _speed = 1, _attackRange = 10, _attackSpeed = 2;
   
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new StayAtRangeStrategy(Target, _agent, _attackRange,  _speed, _attackSpeed,transform);
    }

    private void Update()
    {
        _behaviorTree.Execute(transform);
    }
}
