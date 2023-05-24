using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackStrategy : IBehaviorNode
{
    private Transform _target;
    private Transform _entityTransform;
    private float _projectileSpeed;
    private float _projectileLifeTime;
    private bool _attackSucceeded;

    public RangeAttackStrategy(Transform target,Transform entityTransform,float projectileSpeed,float projectileLifeTime)
    {
        this._target = target;
        this._attackSucceeded = false;     
        this._entityTransform = entityTransform;
        this._projectileSpeed = projectileSpeed;
        this._projectileLifeTime = projectileLifeTime;
    }

    public IBehaviorNode.NodeState Execute()
    {
        if (_target != null)
        {
            //fire logic
           // Debug.DrawLine(_entityTransform.transform.position, _target.position,Color.red,3f);

            // object pooling in projectile pool
            GameObject projectile = ProjectilePool.Instance.GetProjectile();
            projectile.transform.position = _entityTransform.position;
            projectile.transform.rotation = _entityTransform.rotation;
            
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.Destination = _target.position;
            projectileController.Launch(_projectileSpeed,_projectileLifeTime);

            _attackSucceeded = true;
            if (_attackSucceeded)
            {
                return IBehaviorNode.NodeState.Success;
            }
            
        }
        return IBehaviorNode.NodeState.Failure;
    }
    public IBehaviorNode.NodeState Stop()
    {
        return IBehaviorNode.NodeState.Success;
    }
}
