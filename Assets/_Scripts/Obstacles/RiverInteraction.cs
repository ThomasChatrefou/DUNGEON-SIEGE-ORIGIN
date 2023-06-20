using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverInteraction : MonoBehaviour
{
    private CharacterDataManager _characterDataManagerComponent;

    // Start is called before the first frame update
    void Start()
    {
        _characterDataManagerComponent = GetComponent<CharacterDataManager>();
    }

    public void SlowCharacterSpeed(float slowdown)
    {
        // do not work, the data speed from character data manager component is used on a awake so it is useless to change it in runtime
        _characterDataManagerComponent.Data.MovementSpeed = _characterDataManagerComponent.Data.MovementSpeed * slowdown;
    }

    public void RemoveSlowCharacterSpeed(float slowdown)
    {
        Debug.Log("exit");
        _characterDataManagerComponent.Data.MovementSpeed = _characterDataManagerComponent.Data.MovementSpeed / slowdown;
    }
}
