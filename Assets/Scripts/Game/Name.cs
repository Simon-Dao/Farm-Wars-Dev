using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Photon.Pun;
using UnityEngine;
public class DebugNameDisplay : MonoBehaviour
{
    [SerializeField] bool showDebugInfoGUI = true;
    [SerializeField] float boxWidth = 150f;
    [SerializeField] float boxHeight = 40f;
    [SerializeField] float boxYOffset = 30f;
    [SerializeField] int fontSize;
    [SerializeField] KeyCode toggleKey = KeyCode.Tab;

    void OnGUI()
    {
        GUI.backgroundColor = Color.clear;
        GUI.skin.box.fontSize = fontSize;
        if (showDebugInfoGUI)
        {
            GameObject[] untaggedObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject targetObject in untaggedObjects)
            {
                ShowObjectNameInGUI(targetObject);
            }
        }
    }

    void Start() {
        showDebugInfoGUI = true;
    }

    void ShowObjectNameInGUI(GameObject targetObject)
    {
        if (Camera.main == null) return;

        // Find the 2D position of the object using the main camera
        Vector2 boxPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);

        // "Flip" it into screen coordinates
        boxPosition.y = Screen.height - boxPosition.y;

        // Center the label over the coordinates
        boxPosition.x -= boxWidth * 0.5f;
        boxPosition.y -= boxHeight + boxYOffset;

        // Draw the box label
        GUI.contentColor = Color.red;
        GUI.Box(new Rect(boxPosition.x, boxPosition.y, boxWidth, boxHeight), targetObject.GetComponent<Player>()._playerID.ToString());
    }
    void Update()
    {
        // Toggle the labels using the specified key
        if (Input.GetKeyDown(toggleKey))
        {
            showDebugInfoGUI = !showDebugInfoGUI;
        }
    }
}
