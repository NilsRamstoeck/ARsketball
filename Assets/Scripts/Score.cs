
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour, EventListener
{
    private int score = 0;

    private void Start() {
        EventTarget.addEventListener(EventType.SCORE_UP, this);
    }

    public void onEvent(Event e) {
        ScoreUpEvent scoreUpEvent = (ScoreUpEvent)e;
        score += scoreUpEvent.amt;
        GetComponent<Text>().text = "" + score;
    }
}
