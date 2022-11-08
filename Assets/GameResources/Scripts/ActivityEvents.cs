using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivityEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent activationEvents;

    [SerializeField]
    private UnityEvent deactivationEvents;

    private void OnEnable() => activationEvents.Invoke();

    private void OnDisable() => deactivationEvents.Invoke();
}
