using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystickFloating : VirtualJoystick
{
    [HideInInspector]
    public RectTransform RectTransform;
    [HideInInspector]
    public RectTransform Knob;

    [SerializeField] private bool hideOnPointerUp = false;
    [SerializeField] private bool centralizeOnPointerUp = true;

    protected override void Awake()
    {
        joystickType = VirtualJoystickType.Floating;
        _hideOnPointerUp = hideOnPointerUp;
        _centralizeOnPointerUp = centralizeOnPointerUp;
        RectTransform = GetComponent<RectTransform>();

        base.Awake();
    }

}
