using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPactSkill : Skill
{
    [SerializeField] private WeaponsController _wc;
    private Health _healthPlayer;
    private float _healthLostPercent;
    private float _dameMulti;
    private float _duration;

    private void Start()
    {
        _id = "blood_pact";
        _CDs = 13;
        _typeSkill = 'E';
        _healthLostPercent = 0.3f;
        _dameMulti = 0.5f;
        _duration = 3f;
        _imgArea.transform.localScale = new Vector3(0, 0, 0);
        InitData();
    }
    private IEnumerator BloodPact()
    {
        _healthPlayer = _player.GetComponent<Health>();
        float _healthLost = _healthPlayer.GetHealthCurrent() * _healthLostPercent;
        //Hiến tế máu
        _healthPlayer.TakeDame(_healthLost);

        //tăng dame
        _wc.AddStartAllWeapon(multix: _dameMulti);    //  %Dame
        yield return new WaitForSeconds(_duration);
        _wc.AddStartAllWeapon(multix: -_dameMulti);    //  %Dame
    }


    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        StartCoroutine(BloodPact());
    }

}
