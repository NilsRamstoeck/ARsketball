using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEvent : Event {

    public string path;

    public LoadEvent(string _path) : base(EventType.LOAD){
        path = _path;
    }

}