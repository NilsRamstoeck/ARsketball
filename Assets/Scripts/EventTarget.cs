using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTarget : MonoBehaviour
{

    private Dictionary<EventType, HashSet<EventListener>> listeners = new Dictionary<EventType, HashSet<EventListener>>();

    public void addEventListener(EventType type, EventListener listener) {
        HashSet<EventListener> eventSet;
        bool gotValue = listeners.TryGetValue(type, out eventSet);
        if (!gotValue) {
            eventSet = new HashSet<EventListener>();
            listeners.Add(type, eventSet);
        }
        eventSet.Add(listener);
    }

    public void removeEventListener(EventType type, EventListener listener) {
        HashSet<EventListener> eventSet;
        bool gotValue = listeners.TryGetValue(type, out eventSet);
        if (!gotValue) {
            return;
        }
        eventSet.Remove(listener);
    }

    public void dispatchEvent(Event e) {
        HashSet<EventListener> eventSet;
        bool gotValue = listeners.TryGetValue(e.type, out eventSet);
        if (!gotValue) {
            return;
        }

        foreach(EventListener listener in eventSet) {
            listener.onEvent(e);
        }
    }
}
