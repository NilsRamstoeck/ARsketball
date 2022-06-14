using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class PositionTracker : MonoBehaviour, EventListener {




    public int amt_frames = 150;

    private Queue<Vector3> path = new Queue<Vector3>();

    enum State { RECORDING, IDLE, REPLAYING }

    private State state = State.IDLE;

    // Start is called before the first frame update
    void Start() {
        state = State.RECORDING;
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
        if (path.Count == amt_frames) {
            //save path
            savePath();
            state = State.IDLE;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void loadPath(string filepath) {
        //
    }

    void savePath() {
        string timestamp = System.DateTime.Now.ToString("ssmmHHddMMyyyy");
        string destination = Application.persistentDataPath + "/" + timestamp + ".dat";
        print(destination);
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, Vector3Serializable.convertVector3Array(path.ToArray()));
        file.Close();
    }

    public void startReplay() {
        state = State.REPLAYING;
    }

    void replay() {
        Vector3 pos = path.Dequeue();
        transform.position = pos;
        if (path.Count == 0) {
            state = State.IDLE;
        }
    }

    void EventListener.onEvent(Event e) {
        switch (e.type) {
            case EventType.START_REPLAY:
                break;
            case EventType.PAUSE_REPLAY:
                break;
            case EventType.LOAD:
                handleLoadEvent((LoadEvent)e);
                break;
            case EventType.SAVE:
                break;
        }
    }

    void handleLoadEvent(LoadEvent e) {

    }
}
