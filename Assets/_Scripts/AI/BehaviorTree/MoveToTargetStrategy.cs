using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetStrategy : IBehaviorTree
{
    private NavMeshAgent _agent;
    private Transform _target;
    private float _speed;
    private float _attackRange;
    private float _attackCooldown;
    private float _lastAttackTime;

    private IBehaviorNode _attackNode;
    private IBehaviorNode _NavMeshMouvNode;

    public MoveToTargetStrategy(Transform target, NavMeshAgent agent, float speed, float attackRange, float attackSpeed)
    {

        this._target = target;
        this._agent = agent;
        
        this._attackRange = attackRange;
        this._attackCooldown = attackSpeed;

        _attackNode = new MeleeAttackStrategy(target);
        _NavMeshMouvNode = new NavMeshMove(target,agent,speed);
    }

    public void Execute(Transform entityTransform)
    {

        if (_target != null)
        {
            
            IBehaviorNode.NodeState mouvState = _NavMeshMouvNode.Execute();

            float distanceToTarget = Vector3.Distance(entityTransform.position, _target.position);
            if (distanceToTarget <= _attackRange)
            {
                mouvState = _NavMeshMouvNode.Stop();
                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    //Debug.Log(_agent.isStopped + " " + distanceToTarget);
                    IBehaviorNode.NodeState attackState = _attackNode.Execute();
                    if (attackState == IBehaviorNode.NodeState.Success)
                    {
                        _lastAttackTime = Time.time;
                    }
                }
            }

        }

    }

}
