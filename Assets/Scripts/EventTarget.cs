using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTarget
{



    static private Dictionary<EventType, HashSet<EventListener>> listeners = new Dictionary<EventType, HashSet<EventListener>>();

    public static void addEventListener(EventType type, EventListener listener) {
        HashSet<EventListener> eventSet;
        bool gotValue = listeners.TryGetValue(type, out eventSet);
        if (!gotValue) {
            eventSet = new HashSet<EventListener>();
            listeners.Add(type, eventSet);
        }
        eventSet.Add(listener);
    }

    public static void removeEventListener(EventType type, EventListener listener) {
        HashSet<EventListener> eventSet;
        bool gotValue = listeners.TryGetValue(type, out eventSet);
        if (!gotValue) {
            return;
        }
        eventSet.Remove(listener);
    }

    public static void dispatchEvent(Event e) {
        HashSet<EventListener> eventSet;
        bool gotValue = listeners.TryGetValue(e.type, out eventSet);
        if (!gotValue) {
            return;
        }

        Queue<EventListener> toRemove = new Queue<EventListener>();

        foreach(EventListener listener in eventSet) {
            try {
                listener.onEvent(e);
            } catch (Exception ex) {
                toRemove.Enqueue(listener);
            }
        }

        while (toRemove.Count != 0) {
            removeEventListener(e.type, toRemove.Dequeue());
        }
    }
}
