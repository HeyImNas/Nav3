using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AgentMovement : MonoBehaviour
{
    Canvas ToggleCanvas;
    private NavMeshAgent agent;
    private LineRenderer pathLine;

    public Transform targetRoom; // The room that the agent will move to
    private GameObject[] roomMarkers; // Array to store the room markers
    private GameObject[] officeMarkers;


    //custom colored lines
    Color c1 = Color.blue;
    Color c2 = Color.black;

    void Start()
    {
        WebGLInput.captureAllKeyboardInput = false;

        // Get or add NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            enabled = false;
            return;
        }

        // Get or add LineRenderer for path visualization
        pathLine = GetComponent<LineRenderer>();

        if (pathLine == null)
        {
            pathLine = gameObject.AddComponent<LineRenderer>();
            pathLine.material = new Material(Shader.Find("Default-Line"));

            pathLine.startColor = c1; //custom line render color
            pathLine.endColor = c2;

            pathLine.startWidth = 0.1f;
            pathLine.endWidth = 0.1f;
        }

        // Set agent properties
        agent.speed = 30f;
        agent.acceleration = 30f;
        agent.angularSpeed = 300f;
        agent.stoppingDistance = 0.5f;
        agent.autoBraking = true;

        // Get all the room markers (gameobjects) in the scene
        roomMarkers = GameObject.FindGameObjectsWithTag("RoomMarker");
        officeMarkers = GameObject.FindGameObjectsWithTag("OfficeMarker");
    }

    void Update()
    {
        // Log when the agent reaches the destination
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Debug.Log("Agent reached the destination.");
        }

        // Update path visualization
        if (pathLine != null && agent.hasPath)
        {
            var path = agent.path;
            pathLine.positionCount = path.corners.Length;
            pathLine.SetPositions(path.corners);
        }
    }

    // Function that updates the target room based on user input
    public void SetTargetRoom(string newTargetName)
    {
        // Find the room marker by name
        GameObject newTarget = null;
        foreach (var marker in roomMarkers)
        {
            if (marker.name.Equals(newTargetName, System.StringComparison.OrdinalIgnoreCase))
            {
                newTarget = marker;
                break;
            }
        }

        if (newTarget != null)
        {
            // Update the target room
            targetRoom = newTarget.transform;
            if (agent != null && targetRoom != null)
            {
                // Set the agent's destination to the new target room
                agent.SetDestination(targetRoom.position);
                Debug.Log("New destination set to: " + targetRoom.position);
            }
        }
        else
        {
            Debug.LogError("Room with name '" + newTargetName + "' not found!");
            
        }
       


    }
}