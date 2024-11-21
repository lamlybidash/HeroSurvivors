using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameObject CharA;
	[SerializeField] private GameObject CharB;
	private GameObject CharacterActive;


	private void Start()
	{
		CharacterActive = CharA;
	}

	void Update()
    {
		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("Nhan A");
			CharB.gameObject.SetActive(false);
			CharA.gameObject.SetActive(true);
			CharA.GetComponent<PlayerMovement>().resetCam();
			CharacterActive = CharA;
		}
		if (Input.GetKey(KeyCode.B))
		{
			Debug.Log("Nhan B");
			CharA.gameObject.SetActive(false);
			CharB.gameObject.SetActive(true);
			CharB.GetComponent<PlayerMovement>().resetCam();
			CharacterActive = CharB;
		}



	}

	public GameObject CharActive()
	{
		return CharacterActive;
	}	






}
