using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public EventType type;
    public GameObject source;

    public Event(EventType _type, GameObject _source) {
        type = _type;
        source = _source;
    }
}
