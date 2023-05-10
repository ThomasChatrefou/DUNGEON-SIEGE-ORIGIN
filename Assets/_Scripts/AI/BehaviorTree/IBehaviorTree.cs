using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBehaviorTree
{
    void Execute(Transform TransformGameobject);
}
