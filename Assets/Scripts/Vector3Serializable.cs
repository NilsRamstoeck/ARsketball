
using System;
using UnityEngine;

[Serializable]
public class Vector3Serializable
{

    float x, y, z;

    Vector3Serializable(Vector3 v) {
        x = v.x;
        y = v.y;
        z = v.z;
    }


    public static Vector3Serializable[] convertVector3Array(Vector3[] vectors) {
        Vector3Serializable[] arr = new Vector3Serializable[vectors.Length];
        int i = 0;
        foreach(Vector3 vector in vectors) {
            arr[i] = new Vector3Serializable(vector);
            i++;
        }
        return arr;
    }

    public static Vector3[] convertVector3Serializable3Array(Vector3Serializable[] vectors) {
        Vector3[] arr = new Vector3[vectors.Length];
        int i = 0;
        foreach (Vector3Serializable vector in vectors) {
            arr[i] = new Vector3(vector.x, vector.y, vector.z);
            i++;
        }
        return arr;
    }
}
