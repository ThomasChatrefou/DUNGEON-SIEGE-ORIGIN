using NaughtyAttributes;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/IntSenderEventChannel", fileName = "IntSenderEventChannel")]
public class IntSenderEventChannelSO : ScriptableObject
{
    public event Action<int> OnEventTrigger;

    [Label("Enable missing listener warning")]
    [SerializeField] private bool _isEventListenerMissingNotified;
    [ShowIf("_isEventListenerMissingNotified")]
    [SerializeField] FailMessagePrinter _failMessagePrinter;

    public void RequestRaiseEvent(int toSend)
    {
        if (OnEventTrigger != null)
        {
            OnEventTrigger.Invoke(toSend);
        }
        else if (_isEventListenerMissingNotified)
        {
            _failMessagePrinter.PrintFailMessage();
        }
    }

    [Space(20)]
    [Header("Debug")]
    [SerializeField] private int _intToSend;

    [Button]
    public void RequestRaiseEvent()
    {
        RequestRaiseEvent(_intToSend);
    }
}
