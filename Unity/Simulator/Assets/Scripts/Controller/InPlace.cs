using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;

public class InPlace : MonoBehaviour
{
    private const int framerate = 10; // number of frames per second

    private const int viewPeriodLen = 3; // number of seconds to rotate to look at target

    private const int placePeriodLen = 1000; // number of seconds to stay in a single location (relavent only for two places)

    private const int periodFrameCount = viewPeriodLen * framerate; // number of frames in a view period

    private const int placePeriodFrameCount = placePeriodLen * framerate; // number of frames in a place period

    private const float maxDegrees = 45f; // maximum degrees that can be rotated from center

    private readonly Vector3[] pos = {new Vector3(148,2,154), new Vector3(152,2,145)}; // positions to teleport to

    public enum PlaceCount {
        One,
        Two
    };

    private Quaternion target;
    private float speed;

    // Params
    public bool pitch = false; // nod head
    public bool yaw = false; // shake head
    public bool roll = false; // tilt head

    public PlaceCount places = PlaceCount.One; // number of places to teleport to

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = framerate;
        transform.position = pos[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if period has ended or if target has been reached
        if (Time.frameCount % periodFrameCount == 0 || transform.rotation == target) {
            speed = Random.Range(0.1f,1.5f); // Adjust speed at which head moves

            // If two places flag is selected and place period has ended
            if (places == PlaceCount.Two && Time.frameCount % placePeriodFrameCount == 0) {
                // Teleport to random position
                transform.position = pos[Random.Range(0,2)];
            }

            // Set new view target
            target = Quaternion.Euler(
                pitch ? Random.Range(-maxDegrees,maxDegrees) : 0,
                yaw ? Random.Range(-maxDegrees,maxDegrees) : 0,
                roll ? Random.Range(-maxDegrees,maxDegrees) : 0);
        }

        // Rotate head towards target, speed is determined by time passed, maximum degrees that can be rotated (which is 2*maxDegrees), the speed multiplier, and the length of the view period
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.fixedDeltaTime * 2 * maxDegrees * speed / viewPeriodLen);
    }
}