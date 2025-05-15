using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupCanvas : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private RectTransform _rect;
    private float _speed = 0.2f;
    private void Start()
    {
        _tmp.fontMaterial = Instantiate(_tmp.fontMaterial);
        _tmp.outlineColor = Color.black;
        _tmp.outlineWidth = 0.15f;
    }
    private void Update()
    {
        _rect.anchoredPosition += new Vector2(0, 1080 * _speed * Time.unscaledDeltaTime);
    }
    public void PopupF(string textx, Vector2? locationx = null)
    {
        if(locationx.HasValue)
        {
            _rect.anchoredPosition = new Vector2(locationx.Value.x, locationx.Value.y);
        }
        else
        {
            _rect.anchoredPosition = new Vector2(0, 0);
        }
        _tmp.SetText(textx);
        transform.gameObject.SetActive(true);
        StartCoroutine(InActivePopup());
    }
    private IEnumerator InActivePopup()
    {
        yield return new WaitForSecondsRealtime(1); // duration 1s
        transform.gameObject.SetActive(false);
    }
}
