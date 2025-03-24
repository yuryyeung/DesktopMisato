using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public enum MouseStatus
    {
        Default, Start, Hold, Move, Tap, End
    }
    public MouseStatus CurrentStatus = MouseStatus.Default;
    public EventSystem Event;
    public List<GameObject> CurrentHover = new List<GameObject>();
    public float TapSec = 0.5f;

    private Vector2 _previousePos;
    public float _timer;

    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(int vKey);
    private const int VK_LBUTTON = 0x01; //鼠标左键
    private const int VK_RBUTTON = 0x02; //鼠标右键

    // Start is called before the first frame update
    void Start()
    {
        _previousePos = Input.mousePosition;
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PointerEventData pointData = new PointerEventData(Event);
        List<RaycastResult> results = new List<RaycastResult>();
        pointData.position = Input.mousePosition;
        Event.RaycastAll(pointData, results);
        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                if (!CurrentHover.Contains(result.gameObject)) CurrentHover.Add(result.gameObject);
            }
        } else CurrentHover.Clear();

        if (GetAsyncKeyState(VK_LBUTTON) != 0 && (CurrentStatus == MouseStatus.Default || CurrentStatus == MouseStatus.End))
        {
            CurrentStatus = MouseStatus.Start;
        } else
        if (CurrentStatus == MouseStatus.Start || CurrentStatus == MouseStatus.Hold || CurrentStatus == MouseStatus.Move)
        {
            if (GetAsyncKeyState(VK_LBUTTON) != 0)
            {
                if ((Vector2)Input.mousePosition == _previousePos) CurrentStatus = MouseStatus.Hold;
                else CurrentStatus = MouseStatus.Move;
            } else {
                if (_timer <= TapSec) CurrentStatus = MouseStatus.Tap;
                else CurrentStatus = MouseStatus.End;
            }
        } else
        if (CurrentStatus == MouseStatus.Tap)
        {
            Debug.Log("Tap!");
            CurrentStatus = MouseStatus.End;
        }
        
    }

    void LateUpdate()
    {
        _timer = (CurrentStatus == MouseStatus.Default || CurrentStatus == MouseStatus.Tap || CurrentStatus == MouseStatus.End) ? 0 : _timer + Time.deltaTime;
        _previousePos = Input.mousePosition;
    }
}
