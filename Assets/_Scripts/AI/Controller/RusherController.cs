using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RusherController : MonoBehaviour
{
    private NavMeshAgent _agent;

    public Transform Target;
    private IBehaviorTree _behaviorTree;
    [SerializeField]
    private float _speed = 1 ,_attackRange=1 , _attackSpeed = 2;




    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new MoveToTargetStrategy(Target,_agent, _speed, _attackRange, _attackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _behaviorTree.Execute(transform);
    }
}
