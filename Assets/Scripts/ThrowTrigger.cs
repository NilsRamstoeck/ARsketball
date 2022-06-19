using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        print(other.name);
    }

}
