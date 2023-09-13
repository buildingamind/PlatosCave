using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Math = System.Math;
// All code here will be within the class

public class Controller_2D_1P : MonoBehaviour {
    /* Variables */
    Vector3 posA = new Vector3(28,2,34);

    private static int framerate = 10;
    int periodFrameCount = 10 * framerate;

    float turnSpeed = 31f;
    int turnDir = 1;
    int rotationCorrection = 1;

    Random rand;

    /* functions */
    void Start () {
        Application.targetFrameRate = framerate;
        rand = new Random();
        transform.position = posA;
    }

    void Update () {

        // if (Time.frameCount % periodFrameCount == 0) {
        //     int oldTurnDir = turnDir;
        //     turnDir = (rand.Next(2) == 1) ? 1 : -1;

        //     if (oldTurnDir == turnDir && Math.Abs(transform.rotation.x) > 22.5f) {
        //         rotationCorrection = -rotationCorrection;
        //     }
        // } else if (Math.Abs(transform.rotation.x) > 22.5f) {
        //     rotationCorrection = -rotationCorrection;
        // }

        // transform.Rotate(Vector3.left * Time.deltaTime * turnSpeed * turnDir * rotationCorrection);
///////////////////////
                // Smoothly tilts a transform towards a target rotation.
        float tiltAngle = 45f;
        int slices = 100;
        float slice = (tiltAngle * Time.frameCount / slices);
        // converts the order of progression from 0 to inf, to 22.5 to -22.5 to 22.5 again
        float angle = Math.Abs((slice%90)-45)-22.5f;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(angle, 0, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime);
  
    }

}