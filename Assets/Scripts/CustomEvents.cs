using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEvent : Event {

    public string path;

    public LoadEvent(string _path, GameObject source) : base(EventType.LOAD, source){
        path = _path;
    }

}

public class ScoreUpEvent : Event {

    public int amt;

    public ScoreUpEvent(int amt, GameObject source) : base(EventType.SCORE_UP, source) {
        this.amt = amt;
    }

}