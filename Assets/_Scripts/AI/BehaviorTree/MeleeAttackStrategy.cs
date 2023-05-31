using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttackStrategy : IBehaviorNode
{
    private int _damage =1; // ça n'a rien a foutre la 
    private Transform _target;
    private bool _attackSucceeded;
    public MeleeAttackStrategy(Transform target)
    {
        this._target = target;
        this._attackSucceeded = false;
    }

    public IBehaviorNode.NodeState Execute()
    {
        if (_target != null)
        {
            //Debug.Log("touché");
            bool proHealth = _target.gameObject.TryGetComponent<ICharacterHealth>(out var health);
            if (proHealth)
            {
                health.TakeDamage(_damage);
                 _attackSucceeded = true;
            }
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
