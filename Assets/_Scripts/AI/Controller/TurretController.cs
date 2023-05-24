using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform Target;
    private IBehaviorTree _behaviorTree;
    private float _attackSpeed = 4,_projectileSpeed = 7f,_projectileLifeTime = 15f;

    private void Start()
    {
        _behaviorTree = new TurretStrategy(Target,_attackSpeed,transform,_projectileSpeed,_projectileLifeTime);
    }

    private void Update()
    {
        _behaviorTree.Execute(transform);
    }
}
