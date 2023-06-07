using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KiterMovetoPlayer : IBehaviorNode
{
    protected BlackBoard _blackBoard;
    NavMeshAgent _agent;
    Transform _target;
    Transform _entityTransform;
    float _range;
    float _speed;
    Vector3 _direction;
    float _distanceToTarget;

    IBehaviorNode _navMeshMove;
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
    public KiterMovetoPlayer(Transform entityTransform,BlackBoard bb)
    {
        SetBlackBoard(bb);
        this._entityTransform = entityTransform;
        _agent = _blackBoard.GetVariable<NavMeshAgent>("agent");
        _target = _blackBoard.GetVariable<Transform>("target");
        _range = _blackBoard.GetVariable<float>("range");
        _speed = _blackBoard.GetVariable<float>("speed");
        _navMeshMove = new NavMeshMove(_target, _agent, _speed);
    }
    public IBehaviorNode.NodeState Execute()
    {
        //Debug.Log("on rentre dans execute de movetoplayer");
        _navMeshMove.Execute();
        return IBehaviorNode.NodeState.Success;
    }
    public IBehaviorNode.NodeState Stop()
    {
        return IBehaviorNode.NodeState.Success;
    }
    public bool Evaluate()
    {      
        _direction = _target.position - _entityTransform.position;
        _direction.y = 0;
        _distanceToTarget = _direction.magnitude;
       //Debug.Log(_distanceToTarget+" range : "+_range );
        if (_distanceToTarget > _range)
        {
            //Debug.Log("MoveToplayer true");
            return true;
        }
        //Debug.Log("MoveToplayer False");
        return false;
    }
}
