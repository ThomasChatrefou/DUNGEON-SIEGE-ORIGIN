using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : IBehaviorNode
{
    private Vector3 _target;
    private NavMeshAgent _agent;
    private bool _mouvementSuccess;
    private float _speed;



    public NavMeshMove(Vector3 target,NavMeshAgent agent,float speed)
    {
        this._target = target;
        this._agent = agent;
        this._speed = speed;
        agent.speed = speed;
        
    }

    public IBehaviorNode.NodeState Execute()
    {
        if(_target !=null && _agent !=null)
        {            
            _agent.isStopped = false;
            _agent.SetDestination(_target);
            _mouvementSuccess = true;
            if (_mouvementSuccess)
            {
                return IBehaviorNode.NodeState.Success;
            }           
        }
        return IBehaviorNode.NodeState.Failure;

    }
    public IBehaviorNode.NodeState Stop()
    {
       
        _agent.speed = 0;   
        _agent.isStopped = true;
        _mouvementSuccess = true;
        if (_mouvementSuccess)
        {
            return IBehaviorNode.NodeState.Success;
        }
        else return IBehaviorNode.NodeState.Failure;
    }
}
