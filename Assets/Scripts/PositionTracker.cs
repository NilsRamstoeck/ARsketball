using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour {




    public int amt_frames = 600;

    private Queue<Vector3> path = new Queue<Vector3>();

    enum State { RECORDING, IDLE, REPLAYING }

    private State state = State.IDLE;

    // Start is called before the first frame update
    void Start() {
    }

    void FixedUpdate() {

        switch (state) {
            case State.IDLE:
                break;
            case State.RECORDING:
                record();
                break;
            case State.REPLAYING:
                replay();
                break;
            default:
                break;
        }


    }

    void record() {
        path.Enqueue(transform.position);
        if(path.Count == amt_frames) {
            //save path
            state = State.IDLE;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void startReplay() {
        state = State.REPLAYING;
    }

    void replay() {
        Vector3 pos = path.Dequeue();
        transform.position = pos;
        if(path.Count == 0) {
            state = State.IDLE;
        }
    }

}
