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
    private IBehaviorNode _navMeshMouvNode;

    public MoveToTargetStrategy(Transform target, NavMeshAgent agent, float speed, float attackRange, float attackSpeed)
    {
        this._target = target;
        this._speed = speed;
        this._agent = agent;       
        this._attackRange = attackRange;
        this._attackCooldown = attackSpeed;
        _attackNode = new MeleeAttackStrategy(target);
        _navMeshMouvNode = new NavMeshMove(target, agent, speed);
    }

    public void Execute(Transform entityTransform)
    {
        if (_target != null)
        {

           
            //Debug.Log(_target.position);
            float distanceToTarget = Vector3.Distance(entityTransform.position, _target.position);
            if (distanceToTarget <= _attackRange)
            {
               
                _navMeshMouvNode.Stop();
                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    //Debug.Log(_agent.isStopped + " " + distanceToTarget);
                    _attackNode.Execute();
                    
                        _lastAttackTime = Time.time;
                    
                }
               
            }
            else
            {
                //_navMeshMouvNode = new NavMeshMove(_target, _agent, _speed);
            }
            _navMeshMouvNode.Execute();

        }

    }

}
