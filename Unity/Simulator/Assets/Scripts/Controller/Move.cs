using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    private NavMeshAgent agent;
    // [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] points;

    private int destPoint = 0;
    // [SerializeField] private Transform target;

    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(points[destPoint].position);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void Start () {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = true;

        GotoNextPoint();
    }



    // Update is called once per frame
    private void Update()
    {
        if (points != null)
        {
            agent.SetDestination(points[0].position);
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
                GotoNextPoint();
        }
    }
}
