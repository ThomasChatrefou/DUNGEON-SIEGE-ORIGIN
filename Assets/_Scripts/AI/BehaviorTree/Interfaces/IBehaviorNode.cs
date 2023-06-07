using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorNode
{
    bool Evaluate();
    
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    NodeState Execute();
    NodeState Stop();
    public void SetBlackBoard(BlackBoard board);
}
