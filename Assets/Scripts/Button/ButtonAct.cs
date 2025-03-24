using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAct : MonoBehaviour
{
    public InputManager Manager;
    public MisatoAction Act;
    public MisatoController Controller;
    public bool Status;
    public bool IsPlay = false;
    public Sprite On, Off;
    public Image ButtonImage;

    public bool ButtonStatus
    {
        get { return Status; }
        set { Status = value; this.gameObject.SetActive(value); }
    }

    // Start is called before the first frame update
    void LateUpdate()
    {
        //Debug.Log(this.gameObject + " : " + Manager.CurrentHover.Contains(this.gameObject));
        if (!Manager.CurrentHover.Contains(this.gameObject)) return;
        switch (Manager.CurrentStatus)
        {
            case InputManager.MouseStatus.Tap: OnTap(); break;
        }
    }

    public void OnTap()
    {
        if (!IsPlay) PlayAction();
        else StopAction();
    }

    public void PlayAction()
    {
        ButtonStatus = false;
        IsPlay = true;
        ButtonImage.sprite = Off;
        Act.Play();
        Controller.StopCounter();
    }

    public void StopAction()
    {
        ButtonStatus = false;
        IsPlay = false;
        ButtonImage.sprite = On;
        Act.Stop();
        Controller.StopCounter();
    }

    public void ChangeState(bool OnOff)
    {
        IsPlay = OnOff;
        ButtonImage.sprite = (OnOff) ? Off : On;
    }
}
