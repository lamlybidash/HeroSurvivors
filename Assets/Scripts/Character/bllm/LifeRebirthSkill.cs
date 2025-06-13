using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRebirthSkill : Skill
{
    private Health _healthPlayer;
    private float _healingPercent;
    private float _duration;

    private void Start()
    {
        _id = "life_rebirth";
        _CDs = 20;
        _typeSkill = 'Q';
        _healingPercent = 0.7f;
        _duration = 5f;
        _imgArea.transform.localScale = new Vector3(0, 0, 0);
        InitData();
    }

    private void LifeRebirth()
    {
        _healthPlayer = _player.GetComponent<Health>();
        float healCount = (_healthPlayer.GetHealthTotal() - _healthPlayer.GetHealthCurrent()) * _healingPercent;
        HealingDurationEffect healDurE = new HealingDurationEffect(healCount, _duration, 0.2f);
        _player.GetComponent<EffectManager>().ExcuteEffect(healDurE);
    }

    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        LifeRebirth();
    }
    //TODO: Continue
}
