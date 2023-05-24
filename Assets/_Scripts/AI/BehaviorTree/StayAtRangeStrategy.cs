using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/*
Stay at Range and auto attack 
*/
public class StayAtRangeStrategy : IBehaviorTree
{
    private NavMeshAgent _agent;
    private Transform _target;
    
    private float _attackrange;
    private float _projectileSpeed;
    private float _projectileLifeTime;
    private float _speed;
    private float _attackCooldown;
    private float _lastAttackTime;
    
    private IBehaviorNode _attackNode;
    private IBehaviorNode _navMeshMouvNode;

    public StayAtRangeStrategy(Transform target,NavMeshAgent agent, float attackrange, float speed, float attackCooldown,Transform entityTransform,float projectileLifeTime,float projectileSpeed)
    {
        this._agent = agent;
        this._target = target;
        this._attackrange = attackrange;
        this._projectileSpeed = projectileSpeed;
        this._projectileLifeTime = projectileLifeTime;
        this._speed = speed;
        this._attackCooldown = attackCooldown;
    
        _attackNode = new RangeAttackStrategy(target, entityTransform,projectileSpeed,projectileLifeTime);
    }

    public void Execute(Transform EntityTransform)
    {
        
        if (_target != null)
        {
            Vector3 direction = _target.position - EntityTransform.position;
            direction.y = 0;
            IBehaviorNode.NodeState mouvState;
            float distanceToTarget = direction.magnitude;
           //Debug.Log(_agent.stoppingDistance - _attackrange);
             if (distanceToTarget < _attackrange+1f && distanceToTarget > _attackrange - 1f)
            {
                if(_navMeshMouvNode != null)
                    _navMeshMouvNode.Stop();

                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    IBehaviorNode.NodeState attackState = _attackNode.Execute();
                    if (attackState == IBehaviorNode.NodeState.Success)
                    {
                        _lastAttackTime = Time.time;
                    }
                }
            }
            else if (distanceToTarget > _attackrange)
            {
               // Debug.Log("on s'approche");
                _navMeshMouvNode = new NavMeshMove(_target.position, _agent, _speed);
            }
            else if (distanceToTarget < _attackrange)
            {
                //Debug.Log("on s'éloigne");
                Vector3 destination = EntityTransform.position - direction.normalized;
                _navMeshMouvNode = new NavMeshMove(destination, _agent, _speed);
                
            }
                mouvState = _navMeshMouvNode.Execute();
          }
    }
}
