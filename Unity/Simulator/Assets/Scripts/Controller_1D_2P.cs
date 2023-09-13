using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
// All code here will be within the class

public class Controller_1D_2P : MonoBehaviour {
    /* Variables */
    Vector3 posA = new Vector3(28,2,34);
    Vector3 posB = new Vector3(29,2,35);

    float turnSpeed = 31f;
    int turnDir = 1;

    Random rand;

    /* functions */
    void Start () {
        Application.targetFrameRate = 10;
        rand = new Random();
    }

    void Update () {

        if (Time.frameCount % 100 == 0) {
            transform.position = (rand.Next(2) == 1) ? posA : posB;

            turnDir = (rand.Next(2) == 1) ? 1 : -1;
        }

        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * turnDir);
    }

}