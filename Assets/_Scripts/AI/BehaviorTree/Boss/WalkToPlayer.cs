using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToPlayer : IBehaviorNode
{
    BlackBoard _blackboard;
    Transform _target;
    NavMeshAgent _agent;
    float _speed;

    IBehaviorNode _mouvNode;

    public WalkToPlayer(BlackBoard blackboard)
    {
        SetBlackBoard(blackboard);
        _target = _blackboard.GetVariable<Transform>("target");
        _agent = _blackboard.GetVariable<NavMeshAgent>("agent");
        _speed = _blackboard.GetVariable<float>("speed");
        _mouvNode = new NavMeshMove(_target, _agent, _speed);
    }
    public void Execute()
    {
        _mouvNode.Execute();
    }
    public bool Evaluate()
    {
        return true;
    }
    public void Stop()
    {

    }
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackboard = bb;
    }
}
