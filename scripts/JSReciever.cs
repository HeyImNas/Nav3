using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSReceiver : MonoBehaviour
{
    public Renderer objectRenderer;

    void Start()
    {
        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();
    }

    public void ChangeColor(string colorName)
    {
        if (colorName.ToLower() == "red")
            objectRenderer.material.color = Color.red;
        else if (colorName.ToLower() == "blue")
            objectRenderer.material.color = Color.blue;
        else
            Debug.Log("Unknown color: " + colorName);
    }
}
