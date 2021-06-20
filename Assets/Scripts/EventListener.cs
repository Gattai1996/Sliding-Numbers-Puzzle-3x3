using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public EventTrigger eventTrigger;
    public UnityEvent unityEvent = new UnityEvent();

    private void OnEnable()
    {
        eventTrigger.AddEventListener(this);
    }

    private void OnDisable()
    {
        eventTrigger.RemoveEventListener(this);
    }

    public void OnEventTrigger()
    {
        unityEvent.Invoke();
    }
}
