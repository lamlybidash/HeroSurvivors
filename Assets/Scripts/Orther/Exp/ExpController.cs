using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
	public static ExpController instance;
	[SerializeField] GameObject _expGf;
	[SerializeField] GameObject _expBf;
	[SerializeField] GameObject _expRf;
	[SerializeField] List<Exp> expsG;
	[SerializeField] List<Exp> expsB;
	[SerializeField] List<Exp> expsR;

	private void Awake()
	{
		if (instance && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
	}


	public void DropExp(Transform location, double expAmount)
	{
		int i = 0;
		if (expAmount <= _expGf.GetComponent<Exp>().GetExpMax())
		{
			i = FindExpIA(1);
			if(i == -1)
			{
				GameObject expNew = Instantiate(_expGf);
				expNew.transform.SetParent(transform);
				expsG.Add(expNew.GetComponent<Exp>());
				expsG[expsG.Count-1].SetUpExp(expAmount);
				expsG[expsG.Count-1].SetUpLocation(location);
				expsG[expsG.Count-1].gameObject.SetActive(true);
			}
			else
			{
				expsG[i].SetUpExp(expAmount);
				expsG[i].SetUpLocation(location);
				expsG[i].gameObject.SetActive(true);
			}
			return;
		}
		if (expAmount <= _expBf.GetComponent<Exp>().GetExpMax())
		{
			i = FindExpIA(2);
			if (i == -1)
			{
				GameObject expNew = Instantiate(_expBf);
				expNew.transform.SetParent(transform);
				expsB.Add(expNew.GetComponent<Exp>());
				expsB[expsB.Count - 1].SetUpExp(expAmount);
				expsB[expsB.Count - 1].SetUpLocation(location);
				expsB[expsB.Count - 1].gameObject.SetActive(true);

			}
			else
			{
				expsB[i].SetUpExp(expAmount);
				expsB[i].SetUpLocation(location);
				expsB[i].gameObject.SetActive(true);
			}
			return;
		}
		if (expAmount <= _expRf.GetComponent<Exp>().GetExpMax())
		{
			i = FindExpIA(3);
			if (i == -1)
			{
				GameObject expNew = Instantiate(_expRf);
				expNew.transform.SetParent(transform);
				expsR.Add(expNew.GetComponent<Exp>());
				expsR[expsR.Count - 1].SetUpExp(expAmount);
				expsR[expsR.Count - 1].SetUpLocation(location);
				expsR[expsB.Count - 1].gameObject.SetActive(true);
			}
			else
			{
				expsR[i].SetUpExp(expAmount);
				expsR[i].SetUpLocation(location);
				expsR[i].gameObject.SetActive(true);
			}
			return;
		}
	}
	public void DestroyAllExp()
	{
		expsB.Clear();
		expsG.Clear();
		expsR.Clear();
	}
	private int FindExpIA(int x) // 1: Green , 2:Blue , 3:Red
	{
		int i = 0;
		switch(x)
		{
			case 1:
				{
					for (i = 0; i < expsG.Count; i++)
					{
						if (expsG[i].isActiveAndEnabled==false)
						{
							return i;
						}
					}	
					break;
				}
			case 2:
				{
					for (i = 0; i < expsB.Count; i++)
					{
						if (expsB[i].isActiveAndEnabled == false)
						{
							return i;
						}
					}
					break;
				}
			case 3:
				{
					for (i = 0; i < expsR.Count; i++)
					{
						if (expsR[i].isActiveAndEnabled == false)
						{
							return i;
						}
					}
					break;
				}
		}	
		return -1;
	}	
}
