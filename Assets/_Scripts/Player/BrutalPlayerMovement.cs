using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class BrutalPlayerMovement : MonoBehaviour, IPlayerMovement
{
    Vector3 move = Vector3.zero;

    public void Move(Vector2 direction, float playerSpeed)
    {
        move = Vector3.Normalize(new Vector3(direction.x, 0, direction.y));

        transform.position += move * Time.deltaTime * playerSpeed;
    }
}
