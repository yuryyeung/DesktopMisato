using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class GameControl : MonoBehaviour
{
    //private static LAppLive2DManager instance;
    private float lastX = -1;
    private float lastY = -1;
    private GameObject MainCamera;
    //Windows接口
    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);
    private const int VK_LBUTTON = 0x01; //鼠标左键
    private const int VK_RBUTTON = 0x02; //鼠标右键

    //动画辅助key
    string AniKey = "Begin";

    // Update is called once per frame
    void Update()
    {
        if (GetAsyncKeyState(VK_LBUTTON) != 0)
        {
            if (AniKey == "Begin")
            {
                lastX = Input.mousePosition.x;
                lastY = Input.mousePosition.y;

                AniKey = "Ani";
            }
            else
            {
                if (lastX == Input.mousePosition.x && lastY == Input.mousePosition.y)
                {
                    return;
                }
                lastX = Input.mousePosition.x;
                lastY = Input.mousePosition.y;

                AniKey = "Begin";
            }
        }
        else
        {
            if (AniKey == "Ani")
            {
                lastX = -1;
                lastY = -1;

                AniKey = "Begin";
            }

        }
    }
}