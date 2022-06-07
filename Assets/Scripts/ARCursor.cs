using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour {
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public GameObject ballToThrow;
    public ARRaycastManager raycastManager;

    public bool useCursor = true;

    private enum GameState { PLACE_COURT, THROW_BALL }

    private GameState state = GameState.PLACE_COURT;

    void Start() {
        cursorChildObject.SetActive(useCursor);
    }

    void Update() {
        print(state);
        switch (state) {
            case GameState.PLACE_COURT:
                placeCourtState();
                break;
            case GameState.THROW_BALL:
                throwBallState();
                break;
            default:
                print("Bad state");
                break;
        }
    }

    void placeCourtState() {
        if (useCursor) {
            UpdateCursor();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            if (useCursor) {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
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
            ballToThrow = GameObject.Instantiate(ballToThrow, position, transform.rotation);
            ballToThrow.GetComponent<Rigidbody>().AddForce((transform.forward * 300 + transform.up * 100));
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
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;

//public class ARCursor : MonoBehaviour {
//    public GameObject cursorChildObject;
//    public GameObject objectToPlace;
//    public GameObject ballToThrow;

//    public ARRaycastManager raycastManager;

//    public bool useCursor = true;

//    private bool courtPlaced = false;

//    void Start() {
//        cursorChildObject.SetActive(useCursor);
//    }

//    void Update() {
//        if (!courtPlaced) {
//            handleCourtNotPlaced();
//        } else {
//            handleCourtPlaced();
//        }

//    }

//    void handleCourtPlaced() {
//        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
//            ballToThrow = GameObject.Instantiate(ballToThrow, transform.position, transform.rotation);
//            ballToThrow.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 5);
//        }
//    }

//    void handleCourtNotPlaced() {
//        if (useCursor) {
//            UpdateCursor();
//        }

//        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
//            if (useCursor) {
//                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
//            } else {
//                List<ARRaycastHit> hits = new List<ARRaycastHit>();
//                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
//                if (hits.Count > 0) {
//                    GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
//                }
//            }
//        }
//    }

//    void UpdateCursor() {
//        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
//        List<ARRaycastHit> hits = new List<ARRaycastHit>();
//        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

//        if (hits.Count > 0) {
//            transform.position = hits[0].pose.position;
//            transform.rotation = hits[0].pose.rotation;
//        }
//    }
//}