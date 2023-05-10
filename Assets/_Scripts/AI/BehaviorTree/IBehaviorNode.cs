using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorNode
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    NodeState Execute();
    

    
}
