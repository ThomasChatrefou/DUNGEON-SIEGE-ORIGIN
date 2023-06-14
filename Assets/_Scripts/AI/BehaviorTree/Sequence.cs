using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : IBehaviorNode
{
    private List<IBehaviorNode> _childNodes = new List<IBehaviorNode>();
    BlackBoard _blackboard;
    public Sequence(params IBehaviorNode[] nodes)
    {
        _childNodes.AddRange(nodes);
    }
    public bool Evaluate()
    {
        foreach (IBehaviorNode node in _childNodes)
        {
            if(!node.Evaluate())
            {
                return false;
            }
        }
        return true;
    }
    public void Execute()
    {
        foreach(IBehaviorNode node in _childNodes)
        {
            node.Execute();
        }
       
    }
    public void Stop()
    {
        
    }
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackboard = bb;
    }
}
