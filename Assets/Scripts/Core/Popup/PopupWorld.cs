using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupWorld : MonoBehaviour
{
    [SerializeField] private TextMeshPro _tmp;
    private float _speed = 2f;
    private void Update()
    {
        transform.position += new Vector3(0, _speed * Time.unscaledDeltaTime, 0);
    }
    public void PopupF(string textx, Vector3? location = null)
    {
        if (location.HasValue)
        {
            transform.position = new Vector3(location.Value.x, location.Value.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
        _tmp.SetText(textx);
        transform.gameObject.SetActive(true);
        StartCoroutine(InActivePopup());
    }
    private IEnumerator InActivePopup()
    {
        yield return new WaitForSeconds(1); // duration 1s
        transform.gameObject.SetActive(false);
    }
}
