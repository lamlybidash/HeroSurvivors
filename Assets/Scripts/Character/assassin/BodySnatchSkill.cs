using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BodySnatchSkill : Skill
{
    [SerializeField] private GameObject _scareCrowPf;
    [SerializeField] private List<GameObject> _listScareCrow;    //List quản lý ScareCrow
    [SerializeField] private GameObject _listSCGO; //List chứa các GameObject ScareCrow
    [SerializeField] private CharacterController _cc;
    private PlayerMovement pm;

    private Health _healthPlayer;
    private int _maxStack = 3;
    private int _stackCurrent = 0;
    private bool addedAction = false;

    private void Start()
    {
        _id = "body_snatch";
        _CDs = 30f;
        _typeSkill = 'Q';
        InitData();
        _cc = GameObject.FindGameObjectWithTag("CharacterController").GetComponent<CharacterController>();
    }
    private void BodySnatch()
    {
        _stackCurrent--;
        _healthPlayer.IncreaseRevive(1);
        foreach (var sc in _listScareCrow)
        {
            if(sc.activeInHierarchy)
            {
                ScareCrow scComp = sc.GetComponent<ScareCrow>();
                scComp.SetPlayer(_player.gameObject);
                _player.position = sc.transform.position;
                pm.DontMove();
                pm.SetHasTarget(false);
                pm.SetCanMove(false);
                scComp.Burn();
                break;
            }
        }    
    }
    private void OnPlayerDie()
    {
        if(_stackCurrent <= 0)
        {
            return;
        }
        BodySnatch();
    }
    private void IncreaseStack()
    {
        if(addedAction == false)
        {
            _healthPlayer = _cc.CharActive().GetComponent<Health>();
            _healthPlayer.OnPlayerDeath += OnPlayerDie;
            pm = _player.GetComponent<PlayerMovement>();
            addedAction = true;
        }
        if (_stackCurrent < _maxStack)
        {
            _stackCurrent++;
            int indexL = FindSCNull();
            if (indexL == -1)
            {
                GameObject SC = Instantiate(_scareCrowPf);
                SC.transform.SetParent(_listSCGO.transform);
                SC.transform.position = _player.position;
                _listScareCrow.Add(SC);
                SC.SetActive(true);
            }
            else
            {
                _listScareCrow[indexL].transform.position = _player.position;
                _listScareCrow[indexL].SetActive(true);
            }
        }
    }
    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        if (!(_stackCurrent < _maxStack))
        {
            _skillCDs.ResetSkillI();
            return;
        }
        IncreaseStack();
    }
    private int FindSCNull()
    {
        int i;
        for (i = 0; i < _listScareCrow.Count; i++)
        {
            if (_listScareCrow[i].activeInHierarchy == false)
            {
                return i;
            }
        }
        return -1;
    }
    public override void ResetSkill()
    {
        base.ResetSkill();
        _stackCurrent = 0;
        addedAction = false;
        foreach (var sc in _listScareCrow)
        {
            sc.SetActive(false);
        }    
    }
}