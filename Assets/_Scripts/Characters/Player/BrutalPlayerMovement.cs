using UnityEngine;

public class BrutalPlayerMovement : MonoBehaviour, IPlayerMovement
{
    private Vector3 _stepMovement = Vector3.zero;
    private bool[] _isConstrained = new bool[2] { false, false };
    private Vector3[] _constraintNormals = new Vector3[2] { Vector3.zero, Vector3.zero };
    private bool _isBlocked => _isConstrained[0] && _isConstrained[1];

    public void Move(Vector2 direction, float playerSpeed)
    {
        _stepMovement = playerSpeed * Time.deltaTime * Vector3.Normalize(new Vector3(direction.x, 0, direction.y));
        
        Vector3 constrainedStep = _stepMovement;
        if (_isBlocked)
        {
            float proj0 = Vector3.Dot(_stepMovement, _constraintNormals[0]);
            float proj1 = Vector3.Dot(_stepMovement, _constraintNormals[1]);

            if (proj0 < 0 && proj1 < 0)
            {
                return;
            }

            if (proj0 < 0)
            {
                constrainedStep -= proj0 * _constraintNormals[0];
            }
            if (proj1 < 0)
            {
                constrainedStep -= proj1 * _constraintNormals[1];
            }
        }

        if (_isConstrained[0])
        {
            float proj = Vector3.Dot(_stepMovement, _constraintNormals[0]);
            if (proj < 0)
            {
                constrainedStep -=  proj * _constraintNormals[0];
            }
        }
        if (_isConstrained[1])
        {
            float proj = Vector3.Dot(_stepMovement, _constraintNormals[1]);
            if (proj < 0)
            {
                constrainedStep -= proj * _constraintNormals[1];
            }
        }

        transform.position += constrainedStep;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_isBlocked)
        {
            if (Vector3.Dot(_stepMovement, _constraintNormals[0]) > 0)
            {
                _isConstrained[0] = false;
                _constraintNormals[0] = Vector3.zero;
            }
            if (Vector3.Dot(_stepMovement, _constraintNormals[1]) > 0)
            {
                _isConstrained[1] = false;
                _constraintNormals[1] = Vector3.zero;
            }
            print("Exit2 : " + _isConstrained[0] + " | " + _isConstrained[1] + _stepMovement);
            return;
        }

        if (_isConstrained[0])
        {
            _isConstrained[0] = false;
            _constraintNormals[0] = Vector3.zero;
            print("Exit1 : " + _isConstrained[0] + " | " + _isConstrained[1] + _stepMovement);
        }

        if (_isConstrained[1])
        {
            _isConstrained[1] = false;
            _constraintNormals[1] = Vector3.zero;
            print("Exit1 : " + _isConstrained[0] + " | " + _isConstrained[1] + _stepMovement);
            return;
        }

        print("Exit0  : " + _isConstrained[0] + " | " + _isConstrained[1] + "-");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 contactNormal = ComputeContactNormalMean(collision);

        if (_isBlocked)
        {
            Debug.LogWarning("Player movement should not have more than 2 contraints");
            return;
        }

        if (_isConstrained[0])
        {
            _isConstrained[1] = true;
            _constraintNormals[1] = contactNormal;
            print("Enter : " + _isConstrained[0] + " | " + _isConstrained[1] + contactNormal);
            return;
        }

        _isConstrained[0] = true;
        _constraintNormals[0] = contactNormal;
        print("Enter : " + _isConstrained[0] + " | " + _isConstrained[1] + contactNormal);
    }

    private Vector3 ComputeContactNormalMean(Collision collision)
    {
        ContactPoint[] contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);

        Vector3 normalMean = Vector3.zero;
        foreach (ContactPoint contact in contacts)
        {
            normalMean += contact.normal;
        }

        normalMean /= collision.contactCount;
        return normalMean;
    }
}
