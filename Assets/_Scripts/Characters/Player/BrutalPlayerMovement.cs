using System.Collections.Generic;
using UnityEngine;

public class BrutalPlayerMovement : MonoBehaviour, IPlayerMovement
{
    private Dictionary<Collider, Vector3> _contactNormalByColliderHitMap = new();

    public void Move(Vector2 direction, float playerSpeed)
    {
        Vector3 stepMovement = playerSpeed * Time.fixedDeltaTime * Vector3.Normalize(new Vector3(direction.x, 0, direction.y));

        int nbConstraints = _contactNormalByColliderHitMap.Count;
        if (nbConstraints == 1 || nbConstraints == 2)
        {
            Dictionary<Collider, Vector3>.Enumerator enumerator = _contactNormalByColliderHitMap.GetEnumerator();
            enumerator.MoveNext();
            Vector3 firstConstraint = enumerator.Current.Value;

            if (nbConstraints == 1)
            {
                stepMovement = Convex2DCollisionHandler.ComputeConstrainedMove(stepMovement, firstConstraint);
            }
            else
            {
                enumerator.MoveNext();
                Vector3 secondConstraint = enumerator.Current.Value;
                stepMovement = Convex2DCollisionHandler.ComputeConstrainedMove(stepMovement, firstConstraint, secondConstraint);
            }
        }
        else if (nbConstraints > 2)
        {
            Debug.LogWarning("MOVE : unhandled constraint state, more than 2 constraints applied on move");
        }

        transform.position += stepMovement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 contactNormal = ComputeContactNormalMean(collision);
        if (_contactNormalByColliderHitMap.TryAdd(collision.collider, contactNormal))
        {
            Debug.Log("ENTER : " + _contactNormalByColliderHitMap[collision.collider] + " | " + _contactNormalByColliderHitMap.Count);
        }
        else
        {
            Debug.LogWarning("ENTER : Trying to add already colliding collider" + contactNormal);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("EXIT   : " + _contactNormalByColliderHitMap[collision.collider] + " | " + (_contactNormalByColliderHitMap.Count - 1));
        _contactNormalByColliderHitMap.Remove(collision.collider);
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
