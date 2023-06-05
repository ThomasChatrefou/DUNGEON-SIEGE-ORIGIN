using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class Convex2DCollisionHandler
{
    public static Vector3 ComputeConstrainedMove(Vector3 moveDirection, Vector3 collisionNormal)
    {
        float proj = Vector3.Dot(moveDirection, collisionNormal);
        if (proj < 0)
        {
            moveDirection -= proj * collisionNormal;
        }
        return moveDirection;
    }

    public static Vector3 ComputeConstrainedMove(Vector3 moveDirection, Vector3 collisionNormal0, Vector3 collisionNormal1)
    {
        Vector3 nConstraint0 = collisionNormal0.normalized;
        Vector3 nConstraint1 = collisionNormal1.normalized;
        Vector3 nMove = moveDirection.normalized;

        float projc = Vector3.Dot(nConstraint0, nConstraint1);
        float proj0 = Vector3.Dot(nMove, nConstraint0);
        float proj1 = Vector3.Dot(nMove, nConstraint1);

        float det = nConstraint0.x * nConstraint1.z - nConstraint0.z * nConstraint1.x;
        if (Mathf.Abs(det) < Mathf.Epsilon)
        {
            if (Mathf.Abs(projc) > 0)
            {
                nMove -= proj0 * nConstraint0;
            }
            else
            {
                Debug.LogError("Convex2DCollisionHandler : One or both collision normals are zero");
                return Vector3.zero;
            }
        }
        else
        {
            float2x2 constraintMatInv = new();
            constraintMatInv.c0.x = nConstraint1.z;
            constraintMatInv.c0.y = -nConstraint0.z;
            constraintMatInv.c1.x = -nConstraint1.x;
            constraintMatInv.c1.y = nConstraint0.x;
            constraintMatInv /= 1.0f / det;

            Vector2 moveInConstraintsCoords = new()
            {
                x = constraintMatInv.c0.x * nMove.x + constraintMatInv.c1.x * nMove.z,
                y = constraintMatInv.c0.y * nMove.x + constraintMatInv.c1.y * nMove.z
            };

            if (moveInConstraintsCoords.x < 0 && moveInConstraintsCoords.y < 0)
            {
                nMove = Vector3.zero;
            }
            else if (proj0 > 0 && proj1 > 0) {} // This is really needed
            else if (moveInConstraintsCoords.x < 0 || (projc <= 0 && proj0 < 0))
            {
                nMove -= proj0 * nConstraint0;
            }
            else if (moveInConstraintsCoords.y < 0 || (projc <= 0 && proj1 < 0))
            {
                nMove -= proj1 * nConstraint1;
            }
        }
        return moveDirection.magnitude * nMove;
    }
};

public class Double2DConstraintDebugger : MonoBehaviour
{
    [BoxGroup("Constraint 0")]
    [OnValueChanged("OnMoveConstraint0AngleChanged")]
    [Range(0.0f, 360.0f)]
    [SerializeField] private float _constraint0Angle = 0.0f;
    [BoxGroup("Constraint 0")]
    [ReadOnly]
    [SerializeField] private Vector3 _constraint0 = Vector3.right;

    [BoxGroup("Constraint 1")]
    [OnValueChanged("OnMoveConstraint1AngleChanged")]
    [Range(0.0f, 360.0f)]
    [SerializeField] private float _constraint1Angle = 0.0f;
    [BoxGroup("Constraint 1")]
    [ReadOnly]
    [SerializeField] private Vector3 _constraint1 = Vector3.right;

    [BoxGroup("Move Direction")]
    [OnValueChanged("OnMoveDirectionAngleChanged")]
    [Range(0.0f, 360.0f)]
    [SerializeField] private float _moveDirectionAngle = 0.0f;
    [BoxGroup("Move Direction")]
    [ReadOnly]
    [SerializeField] private Vector3 _moveDirection = Vector3.right;
    [ReadOnly]
    [SerializeField] private Vector3 _constrainedMove = Vector3.right;

    [SerializeField] private float _smallVectorsMagnitude = 1.0f;
    [SerializeField] private float _bigVectorsMagnitude = 2.0f;

    private Color _moveDirectionColor = Color.green;

    private Vector3 _constraint0Right = Vector3.forward;
    private Vector3 _constraint0Fwd = Vector3.forward;
    private Vector3 _constraint0Left = Vector3.left;
    private Vector3 _constraint0Back = Vector3.back;

    private Vector3 _constraint1Right = Vector3.forward;
    private Vector3 _constraint1Fwd = Vector3.forward;
    private Vector3 _constraint1Left = Vector3.left;
    private Vector3 _constraint1Back = Vector3.back;

    private void OnMoveConstraint0AngleChanged()
    {
        OnAngleChanged(_constraint0Angle, ref _constraint0, ref _constraint0Right, ref _constraint0Fwd, ref _constraint0Left, ref _constraint0Back);
        _constrainedMove = Convex2DCollisionHandler.ComputeConstrainedMove(_moveDirection, _constraint0, _constraint1);
        ChooseColor();
    }
    
    private void OnMoveConstraint1AngleChanged()
    {
        OnAngleChanged(_constraint1Angle, ref _constraint1, ref _constraint1Right, ref _constraint1Fwd, ref _constraint1Left, ref _constraint1Back);
        _constrainedMove = Convex2DCollisionHandler.ComputeConstrainedMove(_moveDirection, _constraint0, _constraint1);
        ChooseColor();
    }
    
    private void OnMoveDirectionAngleChanged()
    {
        OnAngleChanged(_moveDirectionAngle, ref _moveDirection);
        _moveDirection = _smallVectorsMagnitude / _bigVectorsMagnitude * _moveDirection;
        _constrainedMove = Convex2DCollisionHandler.ComputeConstrainedMove(_moveDirection, _constraint0, _constraint1);
        ChooseColor();
    }

    private void ChooseColor()
    {
        float finalMagnitude = _constrainedMove.magnitude;
        if (finalMagnitude == 0)
        {
            _moveDirectionColor = Color.red;
        }
        else if (finalMagnitude == _moveDirection.magnitude)
        {
            _moveDirectionColor = Color.green;
        }
        else
        {
            _moveDirectionColor = Color.blue;
        }
    }

    private void OnAngleChanged(float newAngle, ref Vector3 newDirection, ref Vector3 newDirectionRight, ref Vector3 newDirectionFwd, ref Vector3 newDirectionLeft, ref Vector3 newDirectionBack)
    {
        Vector3[] vectorsToUpdate = { newDirectionRight, newDirectionFwd, newDirectionLeft, newDirectionBack };
        for (int i = 0; i < 4; ++i)
        {
            OnAngleChanged(newAngle, ref vectorsToUpdate[i]);
            newAngle += 90.0f;
        }

        newDirection = _smallVectorsMagnitude / _bigVectorsMagnitude * vectorsToUpdate[0];
        newDirectionRight = vectorsToUpdate[0];
        newDirectionFwd = vectorsToUpdate[1];
        newDirectionLeft = vectorsToUpdate[2];
        newDirectionBack = vectorsToUpdate[3];
    }

    private void OnAngleChanged(float newAngle, ref Vector3 newDirection)
    {
        newDirection.x = Mathf.Cos(Mathf.Deg2Rad * newAngle);
        newDirection.z = Mathf.Sin(Mathf.Deg2Rad * newAngle);
        newDirection *= _bigVectorsMagnitude;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position;

        Gizmos.color = Color.grey;
        Gizmos.DrawRay(origin, _constraint0Right);
        Gizmos.DrawRay(origin, _constraint0Left);

        Gizmos.DrawRay(origin, _constraint1Right);
        Gizmos.DrawRay(origin, _constraint1Left);

        float sin = Mathf.Sin(Mathf.Deg2Rad * Vector3.SignedAngle(_constraint0, _constraint1, Vector3.down));
        if (sin >= 0)
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawRay(origin, _constraint0Back);
            Gizmos.DrawRay(origin, _constraint1Fwd);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(origin, _constraint0Fwd);
            Gizmos.DrawRay(origin, _constraint1Back);
        }
        else
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawRay(origin, _constraint0Fwd);
            Gizmos.DrawRay(origin, _constraint1Back);

            Gizmos.color = Color.black;
            Gizmos.DrawRay(origin, _constraint0Back);
            Gizmos.DrawRay(origin, _constraint1Fwd);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, _constraint0);
        Gizmos.DrawRay(origin, _constraint1);

        Gizmos.color = _moveDirectionColor;
        Gizmos.DrawRay(origin, _moveDirection);
        Gizmos.DrawRay(origin, _constrainedMove);
    }
}
