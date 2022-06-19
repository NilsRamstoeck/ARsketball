using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour, EventListener {
    public Transform cameraTransform;
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public GameObject ballToThrow;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    private enum GameState { PLACE_COURT, THROW_BALL, IDLE, REPLAY }

    private GameState state = GameState.PLACE_COURT;

    private GameObject ball;

    void Start() {
        cursorChildObject.SetActive(useCursor);
        EventTarget.addEventListener(EventType.RESET, this);
    }

    void Update() {
        switch (state) {
            case GameState.PLACE_COURT:
                placeCourtState();
                break;
            case GameState.THROW_BALL:
                throwBallState();
                break;
            default:
                //print("Bad state");
                break;
        }
    }

    void placeCourtState() {
        if (useCursor) {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            if (useCursor) {
                GameObject court = GameObject.Instantiate(objectToPlace, transform.position, Quaternion.LookRotation(cameraTransform.right, Vector3.up));
                print(court.transform.rotation);

                cursorChildObject.SetActive(false);
                state = GameState.THROW_BALL;
            } else {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                if (hits.Count > 0) {
                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
                    state = GameState.THROW_BALL;
                }
            }
        }
    }

    void throwBallState() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            Vector3 position = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
            ball = GameObject.Instantiate(ballToThrow, cameraTransform);
            ball.GetComponent<Rigidbody>().AddForce((cameraTransform.forward * 3000 + cameraTransform.up * 1000));
            state = GameState.IDLE;
            EventTarget.dispatchEvent(new Event(EventType.BALL_THROWN, gameObject));
        }
    }

    void UpdateCursor() {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0) {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    public void onEvent(Event e) {
        if (!ball) return;
        Destroy(ball);
        state = GameState.THROW_BALL;
    }
}
