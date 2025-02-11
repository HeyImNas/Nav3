using UnityEngine;
using UnityEngine.AI;

public class PathDrawer : MonoBehaviour
{
    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    Color c1 = Color.blue;
    Color c2 = Color.black;

    void Start()
    {
        
        // Get NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the GameObject!");
            enabled = false;
            return;
        }

        // Get or add LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

            lineRenderer.startColor = c1; //custom line render color
            lineRenderer.endColor = c2;  
            
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
    }

    void Update()
    {
        if (agent != null && agent.hasPath)
        {
            // Update LineRenderer to visualize the agent's path
            NavMeshPath path = agent.path;
            if (path.corners.Length > 0)
            {
                lineRenderer.positionCount = path.corners.Length;
                lineRenderer.SetPositions(path.corners);
            }
        }
    }
}