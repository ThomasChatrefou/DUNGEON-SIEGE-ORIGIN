using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBaseController : MonoBehaviour
{
    public Transform Target;

    [BoxGroup("Listen to")]
    [SerializeField] VoidEventChannelSO _enemyHitEventChannel;

    protected IBehaviorTree _behaviorTree;

    private void OnEnable()
    {
        //_enemyHitEventChannel.OnEventTrigger += TakeDamage;
    }

    private void OnDisable()
    {
        //_enemyHitEventChannel.OnEventTrigger -= TakeDamage;
    }

    private void Update()
    {
        if (Target != null)
        {
            _behaviorTree.Execute(transform);
        }
    }
}
