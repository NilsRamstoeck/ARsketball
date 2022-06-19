using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{

    private void OnTriggerExit(Collider other) {
        EventTarget.dispatchEvent(new ScoreUpEvent(1, gameObject));
    }

}
