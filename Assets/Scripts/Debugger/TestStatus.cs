using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestStatus : MonoBehaviour
{
    public Text TextArea;
    public InputManager Manager;

    // Update is called once per frame
    void Update()
    {
        string hover = "";
        foreach (GameObject obj in Manager.CurrentHover) hover += obj.name + " ";
        TextArea.text = "MousePos: ( " + Input.mousePosition.x + ", " + Input.mousePosition.y + 
            ")\nHover: " + hover  + "\nCurrent Status: " + Manager.CurrentStatus;
    }
}
