using System.Collections.Generic;
using UnityEngine;

public class BrutalPlayerMovement : MonoBehaviour, IPlayerMovement
{
    Vector3 move = Vector3.zero;

    public void Move(Vector2 direction, float playerSpeed)
    {
        move = Vector3.Normalize(new Vector3(direction.x, 0, direction.y));

        transform.position += playerSpeed * Time.deltaTime * move;
    }

    private void OnCollisionExit(Collision collision)
    {
        Vector3 contactNormal = ComputeContactNormalMean(collision);
        print("Exit : " + contactNormal);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 contactNormal = ComputeContactNormalMean(collision);
        print("Enter : " + contactNormal);
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
