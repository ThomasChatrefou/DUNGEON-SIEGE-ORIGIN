using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(CharacterDataManager))]
public class KiterController : AIBaseController
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _attackRange = 20;
    [SerializeField] private float _attackSpeed = 2;
    [SerializeField] private float _projectileSpeed = 5;
    [SerializeField] private float _projectileLifeTime = 4;
    
    private NavMeshAgent _agent;
   
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new StayAtRangeStrategy(Target, _agent, _attackRange, _speed, _attackSpeed, transform, _projectileSpeed,_projectileLifeTime);
    }
}
