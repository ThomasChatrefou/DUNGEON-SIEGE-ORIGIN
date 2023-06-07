using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class KiterStrategy : IBehaviorTree
{
    private BlackBoard _blackboard;
    private Transform _entityTransform;
    IBehaviorNode root;

    private NavMeshAgent _agent;
    private Transform _target;
    private float _deltaRange = 1;
    private float _attackrange = 30;
    private float _projectileSpeed = 3;
    private float _projectileLifeTime = 4;
    private float _speed = 3;
    private float _attackCooldown=3;
    private float _lastAttackTime;

    
    public KiterStrategy(Transform entityTransform,NavMeshAgent agent,Transform target)
    {
        _entityTransform = entityTransform;
        _agent = agent;
        _target = target;
        _blackboard = new BlackBoard();
        _blackboard.SetVariable<NavMeshAgent>("agent", _agent);
        _blackboard.SetVariable<Transform>("target", _target);
        _blackboard.SetVariable<Transform>("entityTransform", entityTransform);
        _blackboard.SetVariable<float>("range", _attackrange);
        _blackboard.SetVariable<float>("deltaRange", _deltaRange);
        _blackboard.SetVariable<float>("speed", _speed);
        _blackboard.SetVariable<float>("attackCooldown", _attackCooldown);
        _blackboard.SetVariable<float>("projectileSpeed", _projectileSpeed);
        _blackboard.SetVariable<float>("projectileLifeTime", _projectileLifeTime);

        root = new Selector(
            new AtRangeNode(_entityTransform, _blackboard),           
            new MoveBackNode(_entityTransform, _blackboard),
            new KiterMovetoPlayer(_entityTransform, _blackboard)
            
            ) ;
        root.SetBlackBoard(_blackboard);
    }
    
    
    public void Execute(Transform entityTransform)
    {
        root.Evaluate();      
        //root.Execute();

    }
    
}
