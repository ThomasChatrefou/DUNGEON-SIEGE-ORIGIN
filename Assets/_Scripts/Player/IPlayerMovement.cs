using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovement
{
    public void Move(Vector2 direction, float playerSpeed);
}
