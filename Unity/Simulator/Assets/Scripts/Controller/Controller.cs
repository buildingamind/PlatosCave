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

    private readonly Vector3[] pos = {new Vector3(148,2,154), new Vector3(152,2,145)};

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
    void Update()
    {
        // Check if period has ended
        if (Time.frameCount % periodFrameCount == 0 || transform.rotation == target) {
            speed = Random.Range(0.1f,1.5f);

            if (places == PlaceCount.Two && Time.frameCount % placePeriodFrameCount == 0){
                transform.position = pos[Random.Range(0,2)];
            }

            target = Quaternion.Euler(
                pitch ? Random.Range(-maxDegrees,maxDegrees) : 0,
                yaw ? Random.Range(-maxDegrees,maxDegrees) : 0,
                roll ? Random.Range(-maxDegrees,maxDegrees) : 0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * maxDegrees * 2 * speed / viewPeriodLen);
    }
}