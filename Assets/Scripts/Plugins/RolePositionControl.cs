using UnityEngine;
using UnityEngine.UI;

public class RolePositionControl : MonoBehaviour
{
    [Header("Masato Size")]
    public float rolewidth = 520;
    public float roleheight = 520;
    [Range(1, 10)]public float Limit = 5;
    public Vector2 StartPoint = new Vector2(0, 0);

    [Header("Objects")]
    public GameObject MainCamera;
    public Image MisatorChan;

    [Header("Private Elements")]
    private float _screenHeight;
    private float _screenWidth;

    void Start()
    {
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;

        MisatorChan.rectTransform.anchoredPosition = new Vector2(StartPoint.x - MisatorChan.rectTransform.sizeDelta.x / 2, StartPoint.y + MisatorChan.rectTransform.sizeDelta.y / 2);
        MisatorChan.rectTransform.localScale = MisatorChan.rectTransform.localScale * Limit / 5;
    }

    void Update()
    {
        //保持角色的大小和位置
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;
        rolewidth = MisatorChan.rectTransform.sizeDelta.x;
        roleheight = MisatorChan.rectTransform.sizeDelta.y;

        //MisatorChan.rectTransform.sizeDelta = new Vector2((frustumWidth - rolewidth) / 2, -(frustumHeight - roleheight) / 2);
        //Vector2 changedSize = new Vector2((_screenWidth - rolewidth) / 2, -(_screenHeight - roleheight) / 2);
        //Debug.Log(changedSize + " ScreenHeight: " + _screenHeight + " | ScreenWidth:" + _screenWidth);
    }
}