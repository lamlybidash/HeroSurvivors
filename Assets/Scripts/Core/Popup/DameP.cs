using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DameP : MonoBehaviour
{
	private Transform _location;
	private int _dame;
	[SerializeField] private TextMeshPro _tmp;
	private float _speed = 2f;

	private void FixedUpdate()
	{
		transform.position += new Vector3(0, _speed * Time.fixedDeltaTime, 0);
	}
	private void PopupF()
	{
		_tmp.SetText(_dame.ToString());
		transform.gameObject.SetActive(true);
		StartCoroutine(InActivePopup());
	}	
	private IEnumerator InActivePopup()
	{
		yield return new WaitForSeconds(1);
		transform.gameObject.SetActive(false);
	}
	public void SetUpPopup(Transform location, int dame)
	{
		transform.position = new Vector3(location.position.x, location.position.y, transform.position.z);
		_dame = dame;
		PopupF();
	}

}
