using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour, EventListener {

    public Button resetButton, replayButton;

    private void Start() {
        EventTarget.addEventListener(EventType.REPLAY_READY, this);
        EventTarget.addEventListener(EventType.RESET, this);
        EventTarget.addEventListener(EventType.BALL_THROWN, this);
    }

    public void StartReplay() {
        EventTarget.dispatchEvent(new Event(EventType.START_REPLAY, this.gameObject));
        print("replay");
    }

    public void Reset() {
        EventTarget.dispatchEvent(new Event(EventType.RESET, this.gameObject));
    }

    public void RestartGame() {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void LoadPath() {

    }

    public void onEvent(Event e) {
        switch (e.type) {
            case EventType.REPLAY_READY:
                print("replay ready");
                replayButton.interactable = true;
                break;
            case EventType.RESET:
                print("reset");
                replayButton.interactable = false;
                break;
            case EventType.BALL_THROWN:
                print("reset ready");
                resetButton.interactable = true;
                break;
            default:
                break;
        }
    }
}
