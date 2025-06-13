using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _pos;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    public event Action<GameObject,GameObject> MoveComplete; // <this, target>
    public event Action<GameObject,GameObject> MoveFailed; // <this, target>

    private void Update()
    {
        MoveF();
    }

    private void MoveF()
    {
        if (_pos != null && _target.gameObject.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance < 0.01f)
            {
                enabled = false;
                MoveComplete?.Invoke(gameObject, _target.gameObject);
                MoveComplete = null;
            }
        }
        else
        {
            enabled = false;
            MoveFailed?.Invoke(gameObject, _target.gameObject);
            MoveFailed = null;
        }    
    }

    public void StartMove(Transform start, Transform target)
    {
        _pos = start;
        _target = target;
        transform.position = _pos.position;
        if (enabled == false)
        {
            enabled = true;
        }
    }

}
