using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    public int order;
    public string nameGuide;
    [SerializeField] private RectTransform _rect;
    private float _widthCanvas;
    private float _heightCanvas;
    private float _width;
    private float _height;

    public void ResetLocation()
    {
        //_rect.anchoredPosition = new Vector2(1, 0);
        //transform.localPosition = new Vector2(Screen.width, 0);
        _width = _rect.sizeDelta.x;
        _height = _rect.sizeDelta.y;
        transform.localPosition = new Vector2((_widthCanvas + _width) / 2, 0);
    }

    public void GoLeft()
    {
        transform.LeanMoveLocalX(-(_widthCanvas + _width) / 2, 0.5f).setIgnoreTimeScale(true).setOnComplete(OnCompleteHide);
    }
    public void GoRight()
    {
        transform.LeanMoveLocalX((_widthCanvas + _width) / 2, 0.5f).setIgnoreTimeScale(true).setOnComplete(OnCompleteHide);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
        transform.LeanMoveLocalX(0, 0.5f).setIgnoreTimeScale(true);
        //LeanTween.move(_rect, new Vector3(0, 0, 0), 0.5f);
    }

    public void SetupSizeCanvas(float x, float y)
    {
        _widthCanvas = x;
        _heightCanvas = y;
    }
    private void OnCompleteHide()
    {
        gameObject.SetActive(false);
    }    
}