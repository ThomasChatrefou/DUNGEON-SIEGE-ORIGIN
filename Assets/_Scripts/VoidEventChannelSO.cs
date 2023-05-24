using NaughtyAttributes;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/VoidEventChannel", fileName = "VoidEventChannel")]
public class VoidEventChannelSO : ScriptableObject
{
    public event Action OnEventTrigger;

    [Label("Enable missing listener warning")]
    [SerializeField] private bool _isEventListenerMissingNotified;
    [ResizableTextArea]
    [EnableIf("_isEventListenerMissingNotified")]
    [SerializeField] private string _warningLog;

    public void RequestRaiseEvent()
    {
        if (OnEventTrigger != null)
        {
            OnEventTrigger.Invoke();
        }
        else if (_isEventListenerMissingNotified)
        {
            PrintRequestFailedMessage();
        }
    }

    protected void PrintRequestFailedMessage()
    {
        Debug.LogWarning(_warningLog);
    }
}
