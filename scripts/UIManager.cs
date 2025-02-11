using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputField roomInputField; // Reference to the Input Field (no longer used externally)
    public Button setTargetButton; // Reference to the Button (optional for internal debugging)
    public AgentMovement agentMovement; // Reference to the AgentMovement script

    void Start()
    {
        WebGLInput.captureAllKeyboardInput = false;
        // Button functionality is optional for internal testing in Unity Editor
        if (setTargetButton != null)
        {
            setTargetButton.onClick.AddListener(OnSetTargetButtonClicked);
        }
    }

    void OnSetTargetButtonClicked()
    {
        if (roomInputField != null)
        {
            string targetRoomName = roomInputField.text.Trim().ToLower();
            ProcessRoomName(targetRoomName);
        }
    }

    // New method to process input room names
    public void ReceiveRoomInputFromJS(string roomName)
    {
        Debug.Log("Received input from HTML: " + roomName);
        ProcessRoomName(roomName.Trim().ToLower());
    }

    private void ProcessRoomName(string roomName)
    {
        if (!string.IsNullOrEmpty(roomName))
        {
            agentMovement.SetTargetRoom(roomName);
        }
        else
        {
            Debug.LogError("Room name is empty or invalid.");
        }
    }
}