using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform expBarTotal;
    [SerializeField] private RectTransform expBarCurrent;
    private float width;
    private float height;

 //   void Start()
 //   {
 //       SetUpLocation();
	//}

 //   private void SetUpLocation()
 //   {
 //       width = Screen.width - 2;
 //       height = width / 34;
 //       expBarTotal.sizeDelta = new Vector2(width, height);
 //       expBarCurrent.sizeDelta = new Vector2(width, height);
 //   }    

}
