using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorNode
{
    bool Evaluate();
    
   
    void Execute();
    void Stop();
    public void SetBlackBoard(BlackBoard board);
}
