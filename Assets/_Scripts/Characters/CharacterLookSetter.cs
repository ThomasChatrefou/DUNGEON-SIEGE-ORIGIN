using UnityEngine;

[RequireComponent(typeof(CharacterDataManager))]
public class CharacterLookSetter : MonoBehaviour
{
    private CharacterDataManager _characterDataManager;

    private void Awake()
    {
        _characterDataManager = GetComponent<CharacterDataManager>();
        Instantiate(_characterDataManager.Data.CharacterLook, transform);
    }
}
