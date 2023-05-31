using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStrategy : IBehaviorTree
{
    private Transform _target;

    private float _attackRange;
    private float _attackCooldown;
    private float _lastAttackTime;
    private float _projectileSpeed;
    private float _projectileLifeTime;
    private IBehaviorNode _attackNode;

    public TurretStrategy(Transform target, float attackCooldown, Transform entityTransform, float projectileSpeed, float projectileLifeTime)
    {
        _target = target;
        _attackCooldown = attackCooldown;
        
        _attackNode = new RangeAttackStrategy(target, entityTransform,projectileSpeed,projectileLifeTime);
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
                if (attackState == IBehaviorNode.NodeState.Success)
                {
                    _lastAttackTime = Time.time;
                }
            }
        }
    }
}
