using UnityEngine;

public class AIBaseController : MonoBehaviour
{
    public Transform Target;

    protected IBehaviorTree _behaviorTree;

    private void Update()
    {
        if (Target != null)
        {
            _behaviorTree.Execute(transform);
        }
    }
}
