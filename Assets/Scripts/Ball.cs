using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class Ball : MonoBehaviour, EventListener {



    public const int amt_frames = 140;

    private Queue<Vector3> currentPath = new Queue<Vector3>();

    private Vector3[] pathArr;

    enum State { RECORDING, IDLE, REPLAYING }

    private State state = State.IDLE;

    // Start is called before the first frame update
    void Start() {
        state = State.RECORDING;
        EventTarget.addEventListener(EventType.START_REPLAY, this);
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
        currentPath.Enqueue(transform.position);
        if (currentPath.Count == amt_frames) {
            pathArr = currentPath.ToArray();
            //save path
            savePath();
            state = State.IDLE;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<SphereCollider>().enabled = false;
            EventTarget.dispatchEvent(new Event(EventType.REPLAY_READY, gameObject));
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
        bf.Serialize(file, Vector3Serializable.convertVector3Array(currentPath.ToArray()));
        file.Close();
        EventTarget.dispatchEvent(new Event(EventType.SAVE, gameObject));
    }

    public void startReplay() {
        currentPath = new Queue<Vector3>(pathArr);
        state = State.REPLAYING;
    }

    void replay() {
        if (currentPath.Count == 0) {
            state = State.IDLE;
            return;
        }
        Vector3 pos = currentPath.Dequeue();
        transform.position = pos;
    }

    void EventListener.onEvent(Event e) {
        print(e);
        switch (e.type) {
            case EventType.START_REPLAY:
                startReplay();
                break;
            case EventType.PAUSE_REPLAY:
                break;
            case EventType.LOAD:
                handleLoadEvent((LoadEvent)e);
                break;
        }
    }

    void handleLoadEvent(LoadEvent e) {

    }
}
