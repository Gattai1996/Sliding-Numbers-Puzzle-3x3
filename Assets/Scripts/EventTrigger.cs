using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event Trigger", menuName = "Event Trigger", order = 52)]
public class EventTrigger : ScriptableObject
{
    private List<EventListener> eventListenersList = new List<EventListener>();

    public void AddEventListener(EventListener eventListener)
    {
        eventListenersList.Add(eventListener);
    }

    public void RemoveEventListener(EventListener eventListener)
    {
        try
        {
            eventListenersList.Remove(eventListener);
        }
        catch (System.Exception exception)
        {
            throw new System.Exception($"Error: {eventListener} are not in {this} eventListenersList. \n {exception}");
        }
    }

    public void Trigger()
    {
        foreach (EventListener eventListener in eventListenersList)
        {
            eventListener.OnEventTrigger();
        }
    }
}
