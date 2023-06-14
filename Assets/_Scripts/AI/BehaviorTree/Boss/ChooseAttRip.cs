using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class ChooseAttRip : IBehaviorNode
{
    BlackBoard _blackboard;

    NavMeshAgent _agent;
    Transform _target;
    Transform _entityTransform;
    float _range;
  
    Vector3 _direction;
    float _distanceToTarget;

    float _cooldownAttack;
    float _lastAction;

    IBehaviorNode _attackNode;
    IBehaviorNode _fightbackNode;
    public ChooseAttRip(BlackBoard blackboard)
    {
        SetBlackBoard(blackboard);
        _target = _blackboard.GetVariable<Transform>("target");
        _agent = _blackboard.GetVariable<NavMeshAgent>("agent");
        _cooldownAttack = _blackboard.GetVariable<float>("cooldownAttack");

        /*
         _attackNode = new 
         _fightbackNode = new 
         */
    }
    public void Execute()
    {
        int rand = Random.Range(0, 1);
        if (rand == 1)
        {
            _attackNode.Execute();
        }
        else
        {
            _fightbackNode.Execute();
        }
    }
    public bool Evaluate()
    {
        _direction = _target.position - _entityTransform.position;
        _direction.y = 0;
        _distanceToTarget = _direction.magnitude;
        
        if (_distanceToTarget < _range)
        {
            if (Time.time - _lastAction >= _cooldownAttack)
            {
                _lastAction = Time.time;
                return true;
            }
        }
        
        return false;
        
    }
    public void Stop()
    {

    }
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
}
