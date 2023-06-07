using UnityEngine;

public class TurretController : AIBaseController
{
    [SerializeField] private float _attackSpeed = 4;
    [SerializeField] private float _projectileSpeed = 3f;
    [SerializeField] private float _projectileLifeTime = 15f;

    private void Start()
    {
        _behaviorTree = new TurretStrategy(Target,_attackSpeed,transform,_projectileSpeed,_projectileLifeTime);
    }
    private void Update()
    {
        _behaviorTree.Execute(transform);
    }
}
