using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<TutorialItem> _listItem;
    [SerializeField] private int _itemCurrent;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private GameController _gc;

    private void Start()
    {
        CloseGuide();
    }

    public void OpenGuide()
    {
        _gc.PauseGame(true);
        _itemCurrent = 0;
        _background.alpha = 0;
        _background.gameObject.SetActive(true);
        _background.LeanAlpha(1, 0.5f).setIgnoreTimeScale(true);
        _listItem[_itemCurrent].ShowPanel();
    }
    public void CloseGuide()
    {
        foreach (TutorialItem item in _listItem)
        {
            item.SetupSizeCanvas(_canvasRect.sizeDelta.x, _canvasRect.sizeDelta.y);
            item.ResetLocation();
            item.gameObject.SetActive(false);
        }
        _itemCurrent = -1;
        _background.LeanAlpha(0, 0.5f).setOnComplete(CloseBG).setIgnoreTimeScale(true);
    }

    private void CloseBG()
    {
        _background.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _gc.PauseGame(false);
    }

    public void NextPanel()
    {
        if(_itemCurrent >= _listItem.Count - 1)
        {
            return;
        }
        _listItem[_itemCurrent].GoLeft();
        _itemCurrent++;
        _listItem[_itemCurrent].ShowPanel();
    }
    public void PrevPanel()
    {
        if (_itemCurrent <= 0)
        {
            return;
        }
        _listItem[_itemCurrent].GoRight();
        _itemCurrent--;
        _listItem[_itemCurrent].ShowPanel();
    }

    //TODO Thêm background làm mờ xung quanh
    //TODO Edit video thêm các panel khác. Hoàn thành(Move)
}
