using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : IBehaviorNode
{
    BlackBoard _blackBoard;
    private List<IBehaviorNode> _childNodes = new List<IBehaviorNode>();
    public Selector(params IBehaviorNode[] nodes)
    {
        _childNodes.AddRange(nodes);
        Debug.Log(_childNodes.Count);
    }
    public bool Evaluate()
    {
        foreach(IBehaviorNode node in _childNodes)
        {
            if(node.Evaluate())
            {
                Debug.Log("on rentre dans evaluate : "+node.ToString());
                node.Execute();
                return true;
            }
        }
        return false;
    }
    public IBehaviorNode.NodeState Execute()
    {
        foreach(IBehaviorNode node in _childNodes)
        {
            if (node.Evaluate())
            {
                Debug.Log("on rentre dans un execute "+node.ToString());
                node.Execute();
               
                return IBehaviorNode.NodeState.Success;
            }
            
            return IBehaviorNode.NodeState.Failure;
        }
        
        return IBehaviorNode.NodeState.Failure;
    }
    public IBehaviorNode.NodeState Stop()
    {
        return IBehaviorNode.NodeState.Success;
    }
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
}
