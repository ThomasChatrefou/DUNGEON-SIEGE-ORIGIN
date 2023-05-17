using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackStrategy : IBehaviorNode
{
    private Transform _target;
   
    private bool _attackSucceeded;
    private Transform _entityTransform;

    public RangeAttackStrategy(Transform target,Transform entityTransform)
    {
        this._target = target;
        this._attackSucceeded = false;     
        this._entityTransform = entityTransform;
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
            projectileController.Launch();

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
