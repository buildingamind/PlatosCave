using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;

public class Controller : MonoBehaviour
{
    private const int framerate = 10;

    private const int viewPeriodLen = 3; // seconds

    private const int placePeriodLen = 1000; // seconds

    private const int periodFrameCount = viewPeriodLen * framerate;

    private const int placePeriodFrameCount = placePeriodLen * framerate;

    private const float maxDegrees = 45f;

    private readonly Vector3[] pos = {new Vector3(28,2,34), new Vector3(20,2,32)};

    public enum PlaceCount {
        One,
        Two,
        Inf};

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
    }

    // Update is called once per frame
    void Update()
    {
        // Check if period has ended

        if (Time.frameCount % periodFrameCount == 0 || transform.rotation == target) {
            speed = Random.Range(0f,1f);

            if (places == PlaceCount.Two && Time.frameCount % placePeriodFrameCount == 0){
                transform.position = pos[Random.Range(0,2)];
            }

            target = Quaternion.Euler(
                pitch ? Random.Range(-maxDegrees,maxDegrees) : 0,
                yaw ? Random.Range(-maxDegrees,maxDegrees) : 0,
                roll ? Random.Range(-maxDegrees,maxDegrees) : 0);

            Debug.Log("target: " + target.eulerAngles);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * maxDegrees * 2 * speed / viewPeriodLen);
        // transform.rotation = Quaternion.Lerp(transform.rotation, target, 1 / (periodFrameCount-t));
        // Debug.Log("rotation: " + transform.rotation);
        Debug.Log("rotation: " + transform.rotation.eulerAngles);

        // Debug.Log("change: " + (transform.rotation * Quaternion.Inverse(oldRotation)));
    }
}

/*
Places: 1, 2, inf
Axes of Rotation: 1, 2, 3
Max Rotation: 90 deg, 360 deg
Degrees_per_frame_min:
Degrees_per_frame_max:
# worlds: forest, room, etc.
*/