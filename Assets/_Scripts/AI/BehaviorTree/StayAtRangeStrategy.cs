using System.Collections;
using System.Collections.Generic;
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
    private GameObject _projectile;
    private float _speed;
    private float _attackCooldown;
    private float _lastAttackTime;

    private IBehaviorNode _attackNode;
    private IBehaviorNode _navMeshMouvNode;

    public StayAtRangeStrategy(Transform target,NavMeshAgent agent, float attackrange, GameObject projectile, float speed, float attackCooldown, float lastAttackTime)
    {
        this._agent = agent;
        this._target = target;
        this._attackrange = attackrange;
        this._projectile = projectile;
        this._speed = speed;
        this._attackCooldown = attackCooldown;
        this._lastAttackTime = lastAttackTime;

        //_attackNode = new RangeAttackStrategy(target,projectile,);
        _navMeshMouvNode = new NavMeshMove(target, agent, speed);
    }

    public void Execute(Transform EntityTransform)
    {
        if (_target != null)
        {
            IBehaviorNode.NodeState mouvState = _navMeshMouvNode.Execute();
            float distanceToTarget = Vector3.Distance(EntityTransform.position, _target.position);
            if (distanceToTarget >= _attackrange)
            {
                mouvState = _navMeshMouvNode.Stop();
                if (Time.time - _lastAttackTime >= _attackCooldown)
                {
                    //IBehaviorNode.NodeState attackState = _attackNode.Execute();
                    if (/*attackState == IbehaviorNode.NodeState.Success*/)
                    {
                        _lastAttackTime = Time.time;
                    }
                }
            }
            else if(distanceToTarget <_attackrange-0.5f)
            {

            }
                
          }
    }
}
