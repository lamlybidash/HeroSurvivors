using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class ASyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _sliderLoading;


    private void Start()
    {
        StartCoroutine(EmulationLoading(2f));
    }


    //Giả lập loading trong t seconds =))
    IEnumerator EmulationLoading(float t)
    {
        float timeCountTemp = 0;
        _sliderLoading.value = 0;
        while (timeCountTemp < t)
        {
            timeCountTemp += Time.unscaledDeltaTime;
            _sliderLoading.value += Time.unscaledDeltaTime / t;
            yield return null;
        }
        _sliderLoading.value = 1f;
        _loadingScreen.SetActive(false);
    }
}
