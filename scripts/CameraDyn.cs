using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DynamicCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform agentTransform; // Reference to the agent (cube)

    public Camera mainCamera; // Reference to the main camera

    [SerializeField]
    private Vector3 baseOffset = new Vector3(0, 2, -3); // Default offset
    [SerializeField]
    private Vector3 closeOffset = new Vector3(0, 1, -1.5f); // Closer offset when moving

    [SerializeField]
    private float speedThreshold = 0.1f; // Speed at which the camera starts to adjust
    [SerializeField]
    private float followSmoothness = 5f; // Smoothness of camera movement


    //camera part
    public float cameraSmoothingFactor = 1;
    public float lookUpMax = 60;
    public float lookUpMin = -60;

    //public Transform t_camera;
    
    private Quaternion camRotation;
    private RaycastHit hit;
    private Vector3 Camera_offset;




    void Start()
    {
        // Get the main camera
        /*
        camRotation = transform.rotation;
        Camera_offset = t_camera.localPosition;
        */

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found in the scene!");
        } /*
        {
            t_camera = mainCamera.transform;
        }*/

        // Ensure the agentTransform is assigned
        if (agentTransform == null)
        {
            Debug.LogError("Agent Transform is not assigned in the Inspector!");
        }
    }

    void LateUpdate()
    {
        if (mainCamera != null && agentTransform != null)
        {
            // Get the agent's velocity to determine the speed
            NavMeshAgent agent = agentTransform.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                float speed = agent.velocity.magnitude;

                // Determine current offset based on speed
                Vector3 currentOffset = speed > speedThreshold ? closeOffset : baseOffset;

                // Update the camera's position with the current offset
                Vector3 desiredPosition = agentTransform.position + agentTransform.TransformDirection(currentOffset);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, Time.deltaTime * followSmoothness);

                // Make the camera look at the agent, keeping the view adjusted vertically
                mainCamera.transform.LookAt(agentTransform.position + Vector3.up * 5.5f); // Adjust the look-at height for better framing
             
            }
        }
    }
    /*void Update()
    {

        camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactor * (-1); //look up and down
        camRotation.y += Input.GetAxis("Mouse X") * cameraSmoothingFactor; //look left and right

        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

        if (Physics.Linecast(transform.position, transform.position+ transform.localRotation*Camera_offset, out hit))
        {
            t_camera.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
        } else
        {
            t_camera.localPosition = Camera_offset;
        }


    }*/

}
