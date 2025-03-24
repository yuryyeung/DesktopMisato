using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MisatoController : MonoBehaviour
{
    public InputManager Manager;
    public MisatoAction Act;
    public bool BtnOnOff = false;
    public GameObject HitBox;
    public Coroutine TapCorotine;
    public ButtonAct PButton;

    public float Timer = 0;
    public float HoldTime = 5;
    private float _nextTimer = 0;
    public bool IsPressed = false;

    void Start()
    {
        _nextTimer += HoldTime;
        PButton.ButtonStatus = false;
    }

    void LateUpdate ()
    {
        if (!Manager.CurrentHover.Contains(HitBox)) return;
        //Debug.Log(this.gameObject + " : " + Manager.CurrentHover.Contains(HitBox) + " - " + Manager.CurrentStatus);

        switch (Manager.CurrentStatus)
        {
            case InputManager.MouseStatus.Start: IsPressed = true; _previousPos = Input.mousePosition; break;
            case InputManager.MouseStatus.Hold: _previousPos = Input.mousePosition; break;
            case InputManager.MouseStatus.Move: if (IsPressed) OnMove(); break;
            case InputManager.MouseStatus.Tap: if (IsPressed) OnTap(); break;
            case InputManager.MouseStatus.End: IsPressed = false; break;
        }
    }

    public Vector2 _previousPos = Vector2.zero;

    public void OnMove()
    {
        RectTransform transform = this.GetComponent<RectTransform>();
        transform.anchoredPosition += (Vector2)Input.mousePosition - _previousPos;
        _previousPos = Input.mousePosition;
    }


    public void OnTap()
    {
        if (TapCorotine != null) { Timer = 0; return; }
        if (!Act.isEnding && !Act.IsStarting) TapCorotine = StartCoroutine(TapAction());
    }

    public IEnumerator TapAction()
    {
        Timer = 0;
        PButton.ButtonStatus = true;
        while (true)
        {
            yield return null;
            Timer += Time.deltaTime;
            if (Timer > HoldTime) break;
        }
        PButton.ButtonStatus = false;
        TapCorotine = null;
    }

    public void StopCounter()
    {
        StopCoroutine(TapCorotine);
        TapCorotine = null;
    }
}
