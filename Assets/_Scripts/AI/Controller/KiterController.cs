using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(CharacterDataManager))]
public class KiterController : AIBaseController
{
    
    private NavMeshAgent _agent;
   
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree = new KiterStrategy(transform, _agent,Target);
    }
   
}
