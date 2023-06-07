using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretStrategy : IBehaviorTree
{
    BlackBoard _blackBoard;
    private Transform _target;
    private bool _attackSucceeded = true;
    private float _attackRange;
    private float _attackCooldown;
    private float _lastAttackTime;
    private float _projectileSpeed;
    private float _projectileLifeTime;
    private IBehaviorNode _attackNode;

    public TurretStrategy(Transform target, float attackCooldown, Transform entityTransform, float projectileSpeed, float projectileLifeTime)
    {
        _blackBoard = new BlackBoard();
        _blackBoard.SetVariable<float>("attackCooldown", attackCooldown);
        _blackBoard.SetVariable<float>("projectileSpeed",projectileSpeed);
        _blackBoard.SetVariable<float>("projectileLifeTime", projectileLifeTime);
        _blackBoard.SetVariable<Transform>("target", target);
        //Debug.Log(target + " " + attackCooldown + " " + projectileSpeed);
        _target = target;
        _attackCooldown = attackCooldown;
        _attackNode = new RangeAttackStrategy(entityTransform, _blackBoard);
        
        _projectileSpeed = projectileSpeed;
        _projectileLifeTime = projectileLifeTime;
    }

    public void Execute(Transform entityTransform)
    {
        if (_target != null)
        {
            if (Time.time - _lastAttackTime >= _attackCooldown)
            {
                IBehaviorNode.NodeState attackState = _attackNode.Execute();
                _attackSucceeded = true;
                if (_attackSucceeded)
                {                 
                    _lastAttackTime = Time.time;
                }
            }     
        }
    }
}
