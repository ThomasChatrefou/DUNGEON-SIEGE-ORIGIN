using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : IBehaviorNode
{
    //private Vector3 _target;
    private Transform _targetTransform;
    private NavMeshAgent _agent;
    private bool _mouvementSuccess;
    private float _speed;

    BlackBoard _blackBoard;

    public NavMeshMove(Transform targetTransform,NavMeshAgent agent,float speed)
    {
        this._targetTransform = targetTransform;
        this._agent = agent;
        this._speed = speed;
        agent.speed = speed;
        
    }

    public void Execute()
    {
        //Debug.Log("l'ia avance");
        if (_targetTransform != null && _agent !=null)
        {
            //Debug.Log("l'ia avance");
            _agent.speed = _speed;
            _agent.isStopped = false;
            _agent.SetDestination(_targetTransform.position);
            _mouvementSuccess = true;
            if (_mouvementSuccess)
            {
               
            }           
        }
        

    }
    public void Stop()
    {
       
        _agent.speed = 0;   
        //_agent.isStopped = true;
        _mouvementSuccess = true;
        if (_mouvementSuccess)
        {
           
        }
       
    }
    public bool Evaluate()
    {
        return true;
    }
    public void SetBlackBoard(BlackBoard bb)
    {
        _blackBoard = bb;
    }
    
}
