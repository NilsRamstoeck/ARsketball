using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{

    private Queue<Vector3> path = new Queue<Vector3>();
    private bool recordPath = true;
    private bool replayPath = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (recordPath && path.Count < 10*60) {
            print(transform.position);
            path.Enqueue(transform.position);
        } else if (recordPath) {
            //TODO: save path
            print("replay");
            recordPath = false;
            replay();
        }

        if (replayPath && path.Count > 0) {
            Vector3 pos = path.Dequeue();
            print(path.Count);
            print(pos);
            transform.position = pos;
        } else if (replayPath) {
            replayPath = false;
        }

    }

    void replay() {
        //allow to load path
        replayPath = true;
    }

}
