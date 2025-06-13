using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingDurationEffect : StatusEffect
{
    private float _healCount;
    private float _healCountStep;
    private Health _healthPlayer;
    public HealingDurationEffect(float healCountx, float durationx, float stepTimex) : base(durationx, stepTimex)
    {
        _healCount = healCountx;
    }


    public override void ApplyEffect(EffectManager manager)
    {
        _healthPlayer = manager.GetComponent<Health>();
        isActive = true;
        _healCountStep = _healCount * stepTime / duration;
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }

    public override void UpdateEffect()
    {
        _healthPlayer.Healling(_healCountStep);
        base.UpdateEffect();
    }
}
