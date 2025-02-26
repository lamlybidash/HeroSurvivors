using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RepulsionSkill : MonoBehaviour
{
	[SerializeField] private SkillCDs _skillCDs;
	[SerializeField] private SpriteRenderer _imgArea;
	[SerializeField] private LayerMask enemyLayer;
	private Coroutine _coroutine;
	private Transform _player;
	private float _distanceMax = 4f;	// phạm vi của vùng đẩy
	private void Start()
	{
		_skillCDs.SetupDataSkill(20);
	}
	private void Update()
	{
		if (Input.GetKey(KeyCode.Q))
		{
			if (_skillCDs.CanUseSkill() == true)
			{
				RepulsionS();
			}
		}
	}

	private void RepulsionS()
	{
		_imgArea.transform.localScale = new Vector3(_distanceMax * 2, _distanceMax * 2, 1);
		_skillCDs.UseSkill();

		_player = transform.parent;
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _distanceMax, enemyLayer);
		foreach (Collider2D enemy in enemies)
		{
			Vector2 knockbackDir = (enemy.transform.position - _player.position).normalized; // Hướng đẩy ra xa
			enemy.GetComponent<EnemyMovement>().AddForce(100, knockbackDir);
		}
	}
}
