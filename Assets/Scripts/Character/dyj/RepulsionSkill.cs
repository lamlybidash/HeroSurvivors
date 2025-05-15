using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsionSkill : Skill
{
	[SerializeField] private LayerMask enemyLayer;
	private Coroutine _coroutine;
	private float _distanceMax = 4f;	// phạm vi của vùng đẩy
	private void Start()
	{
        _id = "repulsion";
        _CDs = 20;
        _typeSkill = 'Q';
        _imgArea.transform.localScale = new Vector3(_distanceMax * 2, _distanceMax * 2, 1);
        InitData();
	}
	private void RepulsionS()
	{
		_imgArea.transform.localScale = new Vector3(_distanceMax * 2, _distanceMax * 2, 1);
		_player = transform.parent;
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _distanceMax, enemyLayer);
		foreach (Collider2D enemy in enemies)
		{
			Vector2 knockbackDir = (enemy.transform.position - _player.position).normalized; // Hướng đẩy ra xa
			enemy.GetComponent<EnemyMovement>().AddForce(100, knockbackDir);
		}
	}
    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        RepulsionS();
    }
}
