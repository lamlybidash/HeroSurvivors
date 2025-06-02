using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealEffect : StatusEffect
{
    private Health _healthPlayer;
    private float _percent;
    private WeaponsController _wc;
    public LifeStealEffect(float percentx, float durationx, float stepTimex) : base(durationx, stepTimex)
    {
        _percent = percentx;
    }

    private void LifeSteal(float damex)
    {
        _healthPlayer.Healling(damex * _percent);
    }

    public override void ApplyEffect(EffectManager manager)
    {
        if (isActive == false)
        {
            _healthPlayer = manager.GetComponent<Health>();
            _wc = GameObject.FindWithTag("WeaponsController")?.GetComponent<WeaponsController>();
            _wc?.AddEventWeapon(LifeSteal);   
            isActive = true;
        }
    }

    public override void RemoveEffect()
    {
        if (isActive == true)
        {
            _wc?.RemoveEventWeapon(LifeSteal);
            isActive = false;
        }
    }

    public override void UpdateEffect()
    {
        base.UpdateEffect();
    }
}
