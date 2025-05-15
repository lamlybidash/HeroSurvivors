using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InforWindow : MonoBehaviour
{
    public static InforWindow Instance;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _CDsText;
    [SerializeField] private TextMeshProUGUI _desText;
    [SerializeField] private TextMeshProUGUI _bonusText;
    [SerializeField] private RectTransform _thisRect;
    [SerializeField] private RectTransform _canvasRect;


    private TMP_FontAsset _font;
    private Vector2 _poiter;
    private Vector2 _offset;
    private Vector2[] _listOffset;
    private bool _isShowing;
    private int itest = 0;
    public Vector2 selectedOffset;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _isShowing = false;

        StartCoroutine(InitializeOffsets());
    }

    private void Update()
    {
        if (_isShowing)
        {
            UpdatePosWindow();
        }
    }
    
    private void UpdatePosWindow()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, null, out _poiter);
        
        // Thử từng góc để tìm vị trí hợp lệ
        selectedOffset = _listOffset[0];
        foreach (Vector2 offset in _listOffset)
        {
            Vector2 targetPos = _poiter + offset;
            if (IsPositionValid(targetPos))
            {
                selectedOffset = offset;
                break;
            }
        }
        _thisRect.anchoredPosition = _poiter + selectedOffset + new Vector2(10, 10);
    }

    private bool IsPositionValid(Vector2 position)
    {
        if (_thisRect == null || _canvasRect == null) return false;

        Vector2 windowSize = _thisRect.sizeDelta;
        Vector2 canvasSize = _thisRect.sizeDelta;

        // Tính toán các cạnh của InforWindow
        float left = position.x;
        float right = position.x + windowSize.x;
        float bottom = position.y;
        float top = position.y + windowSize.y;

        // Tính toán ranh giới Canvas
        float canvasLeft = -canvasSize.x / 2;
        float canvasRight = canvasSize.x / 2;
        float canvasBottom = -canvasSize.y / 2;
        float canvasTop = canvasSize.y / 2;

        // Kiểm tra xem InforWindow có nằm hoàn toàn trong Canvas không
        return left >= canvasLeft && right <= canvasRight && bottom >= canvasBottom && top <= canvasTop;
    }
    private IEnumerator InitializeOffsets()
    {
        while (_thisRect.sizeDelta.x == 0 && _thisRect.sizeDelta.y == 0)
        {
            yield return null;
        }
        _offset = new Vector2(0, 0);
        _listOffset = new Vector2[]{
            new Vector2(0, 0), //bot left
            new Vector2(-_thisRect.sizeDelta.x, 0), //bot right
            new Vector2(0, -_thisRect.sizeDelta.y),  //top left
            new Vector2(-_thisRect.sizeDelta.x, -_thisRect.sizeDelta.y)   //top right
        };
        gameObject.SetActive(false);
    }
    public void SetupInfor(string titlex, string cdsx, string desx, string bonusx)
    {
        _titleText.text = titlex;
        _CDsText.text = cdsx;
        _desText.text = desx;
        _bonusText.text = bonusx;
    }

    public void ShowInfor()
    {
        _isShowing = true;
        gameObject.SetActive(true);
        itest++;
    }
    
    public void SetupFont(TMP_FontAsset fontx)
    {
        _font = fontx;
        _titleText.font = _font;
        _CDsText.font = _font;
        _desText.font = _font;
        _bonusText.font = _font;
    }
    public void HideInfor()
    {
        _isShowing = false;
        gameObject.SetActive(false);
    }
}
