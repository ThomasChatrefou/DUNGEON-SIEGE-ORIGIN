using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttackStrategy : IBehaviorNode
{
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
            Debug.Log("touché");
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
