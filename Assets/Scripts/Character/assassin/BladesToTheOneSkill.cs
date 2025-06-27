using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladesToTheOneSkill : Skill
{
    [SerializeField] private List<Blade> _blades;
    private EnemyController _ec;
    private int _countPerCircle1;
    private int _countPerCircle2;
    private int _countCompleteTwo;
    private List<GameObject> _listEnemyActive;
    private Vector2 _center;
    private bool _isEnemyDie;
    private void Start()
    {
        _id = "blades_to_the_one";
        _CDs = 15f;
        _typeSkill = 'E';
        InitData();
        _ec = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        SetupBlades();
    }

    private void SetupBlades()
    {
        int i, j;
        float angleTemp;
        i = 0;
        _center = transform.position;
        _countPerCircle1 = _blades.Count / 3;   //Chiếm 1 phần
        _countPerCircle2 = _blades.Count - _countPerCircle1;    //Chiếm 2 phần

        j = 0;
        for (i = 0; i < _countPerCircle1; i++)
        {
            angleTemp = j * (2 * Mathf.PI) / _countPerCircle1;
            _blades[i].SetupData(_center, angleTemp, 1, this); //Tâm trận, góc đặt kiếm, thứ tự vòng
            j++;
        }

        j = 0;
        for (i = _countPerCircle1; i < _blades.Count; i++)
        {
            angleTemp = j * (2 * Mathf.PI) / _countPerCircle2;
            _blades[i].SetupData(_center, angleTemp, 2, this);
            j++;
        }
    }
    private void BladesToTheOne()
    {
        _countCompleteTwo = 0;
        foreach (var item in _blades)
        {
            item.UpdateCenter(transform.position);
            item.gameObject.SetActive(true);
            item.ExcuteBlade();
        }
    }
    public void OnBladeCompleteTwo()
    {
        _countCompleteTwo++;
        if(_countCompleteTwo < _blades.Count)
        {
            return;
        }

        //Tìm enemy xa nhất
        GameObject enemyFarthest = FindEnemyFarthest();
        enemyFarthest.GetComponent<HealthEnemy>().OnEnemyDie += OnEnemyDieF;
        foreach (var item in _blades)
        {
            item.SetTargetThree(enemyFarthest);
            item.SetStatusTargetThreeIsDie(false);
        }
    }
    private void OnEnemyDieF()
    {
        foreach (var item in _blades)
        {
            item.SetStatusTargetThreeIsDie(true);
        }
    }
    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        BladesToTheOne();
    }
    private GameObject FindEnemyFarthest()
    {
        _listEnemyActive = _ec.GetListEnemyActive();
        float maxDistance;
        float distance;
        maxDistance = float.MinValue;
        GameObject enemyFarthest = new GameObject();
        foreach (GameObject enemy in _listEnemyActive)
        {
            if (!enemy.activeInHierarchy)
            {
                continue; // Bỏ qua enemy đã bị chết
            }

            distance = Vector2.Distance(enemy.transform.position, _center);
            if (distance > maxDistance)
            {
                distance = maxDistance;
                enemyFarthest = enemy;
            }
        }
        return enemyFarthest;
    }    
}
