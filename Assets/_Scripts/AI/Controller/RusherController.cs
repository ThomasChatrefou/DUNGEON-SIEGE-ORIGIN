using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RusherController : AIBaseController
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _attackRange = 1;
    [SerializeField] private float _attackSpeed = 2;
    
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new MoveToTargetStrategy(Target,_agent, _speed, _attackRange, _attackSpeed);
    }
}
