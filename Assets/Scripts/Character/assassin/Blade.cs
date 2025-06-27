using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blade : MonoBehaviour
{
    private Vector3 _longTarget;
    private Vector3 _target;
    private Vector3 _center;
    private Vector3 _direct;
    private float _dame = 100;
    private float _radiusConst = 2;
    private float _VConst = 10; //Tốc độ dài cố định
    private float _radius;
    private float _angle;
    private float _angleBlade;
    private float _speed;   //Tốc độ góc
    private int _indexCicle;
    private int _clockwise;
    private BladesToTheOneSkill _skill;
    private GameObject _enemyFarthest;
    private bool _isEnemyDie;

    void Start()
    {
        //transform.position = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _radius;
    }

    void Update()
    {

    }
    
    public void SetupData(Vector2 center, float angle, int  indexCicle, BladesToTheOneSkill skill)
    {
        _center = center;
        _angle = angle;
        _radius = _radiusConst * indexCicle;
        _speed = _VConst / _radius;
        _clockwise = indexCicle % 2 == 0 ? 1 : -1;
        transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _radius;
        _skill = skill;
    }

    private void UpdateRotation(Vector2 direct)
    {
        float angleBlade;
        angleBlade = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleBlade);
    }

    private IEnumerator PhaseOne()  //Phóng kiếm từ tâm trận tới điểm chỉ định
    {
        transform.position = _center;
        _longTarget = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _radius + _center;
        float distance;
        float v; // vận tốc bay từ tâm tới đích
        float t; // thời gian bay từ tâm tới đích
        t = 0.5f;
        v = _radius / t;
        _direct = (_longTarget - transform.position).normalized;
        distance = Vector3.Distance(_longTarget, transform.position);
        while (distance > 0.1f)
        {
            transform.position += _direct * v * Time.deltaTime;
            distance = Vector3.Distance(_longTarget, transform.position);
            UpdateRotation(_direct);
            yield return null;
        }
        StartCoroutine(PhaseTwo());
    }
    private IEnumerator PhaseTwo()  //Kiếm xoay quanh tâm trận
    {
        float timeMax, timeCount;
        timeMax = 2f;
        timeCount = 0;
        while (timeCount < timeMax)
        {
            _angle += Time.deltaTime * _speed * _clockwise;
            _target = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _radius + _center;
            _direct = (_target - transform.position).normalized;
            transform.position = new Vector3(_target.x, _target.y, _target.z);
            UpdateRotation(_direct);
            timeCount += Time.deltaTime;
            yield return null;
        }
        _skill.OnBladeCompleteTwo();

        StartCoroutine(PhaseThree());
    }

    private IEnumerator PhaseThree()  //Phóng kiếm từ vị trí hiện tại tới một mục tiêu
    {
        float timeDelay, timeCount;
        float distance;

        timeDelay = 1f;// Thời gian delay trước khi bay vào mục tiêu
        timeCount = 0;
        while (_enemyFarthest == null)
        {
            yield return null;
        }
        Debug.Log("Find Enemy");
        _longTarget = _enemyFarthest.transform.position;
        _direct = (_longTarget - transform.position).normalized;
        UpdateRotation(_direct);

        while (timeCount < timeDelay)
        {
            if(!_isEnemyDie)
            {
                _longTarget = _enemyFarthest.transform.position;
                _direct = (_longTarget - transform.position).normalized;
                UpdateRotation(_direct);
            }
            timeCount += Time.deltaTime;
            yield return null;
        }

        distance = Vector3.Distance(_longTarget, transform.position);
        while (distance > 0.1f)
        {
            if (!_isEnemyDie)
            {
                _longTarget = _enemyFarthest.transform.position;
                _direct = (_longTarget - transform.position).normalized;
                UpdateRotation(_direct);
            }
            transform.position += _direct * _VConst * Time.deltaTime;
            distance = Vector3.Distance(_longTarget, transform.position);
            yield return null;
        }
        gameObject.SetActive(false);
        _enemyFarthest = null;
    }

    public void UpdateCenter(Vector2 center)
    {
        _center = center;
    }    
    public void ExcuteBlade()
    {
        StartCoroutine(PhaseOne());
    } 

    public void SetTargetThree(GameObject ef)
    { 
        _enemyFarthest = ef;
    }
    public void SetStatusTargetThreeIsDie(bool isDie)
    {
        _isEnemyDie = isDie;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            HealthEnemy hE;
            hE = collision.GetComponent<HealthEnemy>();
            if (hE)
            {
                hE.TakeDame(_dame);
            }
        }
    }
}
