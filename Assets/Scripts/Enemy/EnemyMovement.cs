using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private Camera _cam;
	private Transform _player;
	private Rigidbody2D _rg;
	private EnemyAttack _enemyAttack;
	private Vector2 _direction;
	private float _speed;
	private float _BaseSpeed;
	private float _areaAttack;
	private float _distance = 0;
	private Vector3 _camBL;
	private Vector3 _camTR;
	//private bool isMove = true;
	private int i = 0; // test
	private void Awake()
	{
		_rg = GetComponent<Rigidbody2D>();
		_enemyAttack = GetComponent<EnemyAttack>();
	}

	private void Start()
	{
	}
	private void Update()
	{
		CamCalc();
		CheckOutCam();
		MoveF();
	}

	private void FixedUpdate()
	{
		//MoveF();
	}
	private void MoveF()
	{
		_distance = Mathf.Sqrt(Mathf.Pow(_player.position.x - transform.position.x, 2) + Mathf.Pow(_player.position.y - transform.position.y, 2));

		i++;
		if (_distance > _areaAttack)
		{
			//Debug.Log("di chuyen " + i);
			_direction = (_player.position - transform.position).normalized;
			//Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + _direction * _speed * Time.deltaTime;
			//transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
			transform.Translate(_direction * _speed * Time.deltaTime);
			//_rg.MovePosition((Vector2)transform.position + _direction * _speed * Time.fixedDeltaTime);
		}
		else // trong pham vi -> tan cong player
		{
			//Debug.Log("tan cong " + i);
			_enemyAttack.GiveDame(_player.gameObject, true);
		}
		FlipF();
	}

	private void CamCalc()
	{
		_camBL = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
		_camTR = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
	}

	private void CheckOutCam()
	{
		if((_camBL.x - 1 <= transform.position.x && transform.position.x <= _camTR.x + 1) && (_camBL.y - 1 <= transform.position.y && transform.position.y <= _camTR.y + 1))
		{
			//Debug.Log("Trong cam");
			return;
		}
		gameObject.SetActive(false);
	}	

	private void FlipF()
	{
		if(_direction.x <0)
		{
			transform.localScale = Vector3.one;
		}
		else
		{
			transform.localScale = new Vector3(-1,1,1);
		}	
	}
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			_enemyAttack.GiveDame(collision.gameObject,false);
		}
	}


	private IEnumerator DelayMove(float t)
	{
		yield return new WaitForSeconds(t);
	}	

	public void SetUpData(Transform player, Camera cam, float speedMove, float areaAttack)
	{
		_player = player;
		_cam = cam;
		_speed = speedMove;
		_BaseSpeed = speedMove;
		_areaAttack = areaAttack;
	}
	public void AddSpeed(float x)
	{
		_speed += x;
	}

	public void MultiSpeed(float x)
	{
		_speed *= x;
	}
	public void ResetSpeed()
	{
		_speed = _BaseSpeed;
	}	

	public void AddForce(float fx, Vector2 directionx)
	{
		_rg.AddForce(directionx * fx, ForceMode2D.Impulse);
	}	
}
