using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NodeNavMeshCoord : IBehaviorNode
{
    BlackBoard _blackBoard;
    public Vector3 Target = new Vector3(0,0,0);
   
    private NavMeshAgent _agent;
    private bool _mouvementSuccess;
    private float _speed;



    public NodeNavMeshCoord(BlackBoard bb)
    {
        SetBlackBoard(bb);
        
        this._agent = _blackBoard.GetVariable<NavMeshAgent>("agent");
        this._speed = _blackBoard.GetVariable<float>("speed");
        _agent.speed = _speed;
        
    }

    public IBehaviorNode.NodeState Execute()
    {
        if( _agent !=null)
        {         
            _agent.speed = _speed;
            _agent.isStopped = false;
            _agent.SetDestination(Target);
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
        //_agent.isStopped = true;
        _mouvementSuccess = true;
        if (_mouvementSuccess)
        {
            return IBehaviorNode.NodeState.Success;
        }
        else return IBehaviorNode.NodeState.Failure;
    }
    public bool Evaluate()
    {
        return true;
    }
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
   
}
