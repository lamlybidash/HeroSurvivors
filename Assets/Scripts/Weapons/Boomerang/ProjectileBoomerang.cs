using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoomerang : Weapons
{
    [SerializeField] private TrailRenderer _trail1;
    [SerializeField] private TrailRenderer _trail2;
    private Vector3 _target;
    private Vector3 _direction = Vector3.zero;
    private bool _isRotating;
    private float _speedRotation = 5;
    private float _angleTemp = 0;
    private float _timeCount = 0;
    private float v0; // Vận tốc ban đầu
    private float g = 15f; //Gia tốc
    private bool _isTurn1 = true;
    //Chú thích
    /*
     - Boomerang bay tới vị trí đã chỉ định. Nếu mục tiêu chết trước lúc bay tới thì vẫn bay 
     tới đó rồi quay lại.
     - Thời gian duy trì sẽ bằng duration.
     - Sau khi hết time duy trì thì báo cho CHA để nạp vào list ready.
     - Nếu player bắt được boomerang thì tiếp tục ném tiếp tới mục tiêu khác (Random) mà không cần 
     phải chờ CDs.
     */

    private void Update()
    {
        //Auto Xoay
        RotationF();
    }

    public void ThrowBoomerang(Vector3 target)
    {
        _speedRotation = 5;
        _target = target;
        _isTurn1 = true;
        transform.position = player.position;
        _direction = (target - player.position).normalized;
        _timeCount = 0;
        gameObject.SetActive(true);
        StartCoroutine(ThrowBoomerang());
    }
    private IEnumerator ThrowBoomerang()
    {
        // Tính vận tốc ban đầu để boomerang có thể bay tới target
        v0 = Mathf.Sqrt(2 * g * (Vector3.Distance(_target, transform.position)));
        float vtemp = v0;
        float xtemp;
        while(_timeCount < duration)
        {
            _timeCount += Time.deltaTime;
            //Bay
            _speedRotation = Mathf.Clamp(_speedRotation - Time.deltaTime / 2, 2, 4);
            xtemp = vtemp * Time.deltaTime - g * Time.deltaTime * Time.deltaTime / 2;
            transform.position += new Vector3(xtemp * _direction.x, xtemp * _direction.y, transform.position.z);
            vtemp = vtemp - g * Time.deltaTime;
            if (vtemp <= 0.1)
            {
                _isTurn1 = false;
            }
            yield return null;
        }
        _trail1.Clear();
        _trail2.Clear();
        transform.parent.gameObject.GetComponent<Boomerang>().AddBoomerangReady(this);
        gameObject.SetActive(false);
    }
    private void RotationF()
    {
        if (!_isRotating)
        {
            _isRotating = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        _angleTemp = (_angleTemp + _speedRotation * 360 * Time.deltaTime) % 360;
        transform.rotation = Quaternion.Euler(0, 0, _angleTemp);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_isTurn1 == true)
            {
                _isTurn1 = false;
                return;
            }
            gameObject.SetActive(false);
            transform.parent.gameObject.GetComponent<Boomerang>().ThrowAgain(this);
        }
        if(collision.tag == "Enemy")
        {
            HealthEnemy hE;
            hE = collision.GetComponent<HealthEnemy>();
            if(hE)
            {
                hE.TakeDame(damage);
            }    
        }
    }
}
