using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoWrathSkill : Skill
{
    // Mỗi lần sử dụng gây 1 tầng cộng dồn cho kẻ địch
    // Gây (scale * n)% hp tối đa cho kẻ địch (n = số cộng dồn kỹ năng Ex: n = 2 -> 4%Hp)
    
    [SerializeField] private List<GameObject> _listFireBall;    //List quản lý fireball
    [SerializeField] private GameObject _fireBallPf;
    [SerializeField] private GameObject _listBallGO; //List chứa các GameObject fireball
    [SerializeField] private LayerMask enemyLayer;
    private float _scaleMultiDame = 5f; //
    private float _distanceMax = 6f;
    private int MaxStack = 10;

    private void Start()
    {
        _id = "volcano_wrath";
        _CDs = 1.5f;
        _typeSkill = 'E';
        _imgArea.transform.localScale = new Vector3(_distanceMax * 2, _distanceMax * 2, 1);
        InitData();
    }

    private void VolcanoWrath()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _distanceMax, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            int i;
            i = FindIB();
            if(i == -1)
            {
                GameObject ball = Instantiate(_fireBallPf);
                ball.transform.SetParent(_listBallGO.transform);
                ball.SetActive(true);
                ball.GetComponent<Movement>().StartMove(transform.parent, enemy.transform);    //StartMove(Transform start, Transform target)
                ball.GetComponent<Movement>().MoveComplete += (p,s) =>
                {
                    p.SetActive(false);
                    Debug.Log($"Set active false {p.name}");
                    Dame(s);
                };
                ball.GetComponent<Movement>().MoveFailed += (p, s) =>
                {
                    p.gameObject.SetActive(false);
                };
            }
            else
            {
                _listFireBall[i].GetComponent<Movement>().StartMove(transform.parent, enemy.transform);
                _listFireBall[i].GetComponent<Movement>().MoveComplete += (p,s) => {
                    p.SetActive(false);
                    Dame(s);
                };

            }
        }
    }

    private void Dame(GameObject enemy)
    {
        int stack = 0;
        float dame;
        stack = 0;
        StackEffect volcanoStack = new StackEffect("volcano", MaxStack, true, 4f, 4f);
        enemy.GetComponent<EffectManager>().ExcuteEffect(volcanoStack);
        stack = volcanoStack._stackCurrent;
        dame = enemy.GetComponent<HealthEnemy>().GetHealthTotal() * _scaleMultiDame * stack / 100;
        enemy.GetComponent<HealthEnemy>().TakeDame(dame);
    }

    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        VolcanoWrath();
    }
    private int FindIB()
    {
        int i;
        for (i = 0; i < _listFireBall.Count; i++)
        {
            if (_listFireBall[i].activeInHierarchy == false)
            {
                return i;
            }    
        }
        return -1;
    }    
}
