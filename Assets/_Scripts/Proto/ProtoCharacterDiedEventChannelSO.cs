using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/CharacterDiedEventChannel", fileName = "ProtoCharacterDiedEventChannel")]
[Serializable]
public class ProtoCharacterDiedEventChannelSO : ScriptableObject
{
    public event Action OnCharacterDied;

    public void RaiseEvent()
    {
        OnCharacterDied?.Invoke();
    }
}
