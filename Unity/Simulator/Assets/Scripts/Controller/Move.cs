using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    private const float min = 0f; // minimum coordinate value
    private const float max = 300f; // maximum coordinate value
    private const float edge = 25f; // distance from edge to avoid

    private const float minDist = 0.5f; // minimum distance to target before finding new target

    private const float walkMin = min + edge;
    private const float walkMax = max - edge;

    private NavMeshAgent agent;

    // Calculates the next point to walk to 
    Vector3 newPoint()
    {
        return new Vector3(Random.Range(walkMin, walkMax), 10, Random.Range(walkMin, walkMax));
    }

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        agent.SetDestination(newPoint());
    }

    // Update is called once per frame
    private void Update() {
        if (!agent.pathPending && agent.remainingDistance < minDist)
            agent.SetDestination(newPoint());
    }
}
