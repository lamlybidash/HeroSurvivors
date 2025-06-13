using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackEffect : StatusEffect
{
    List<StatusEffect> listEffect;
    public string _idStack;
    private int _maxStack;
    public int _stackCurrent;
    private bool _hasRefresh;

    public StackEffect(string idStack,int maxStack, bool hasRefresh , float durationx, float stepTimex) : base(durationx, stepTimex)
    {
        _stackCurrent = 1;
        _idStack = idStack;
        _maxStack = maxStack;
        _hasRefresh = hasRefresh;
    }

    public override void ApplyEffect(EffectManager manager)
    {
        if (isActive == false)
        {
            isActive = true;
        }
        listEffect = manager.listEffectAcive;
        StackEffect itemx;
        foreach (StatusEffect item in listEffect)
        {
            if(item.GetType() == typeof(StackEffect) && item != this)
            {
                itemx = (StackEffect)item;
                if (itemx._idStack == _idStack)
                {
                    _stackCurrent = itemx._stackCurrent;
                    if(_stackCurrent < _maxStack)
                    {
                        _stackCurrent++;
                    }
                    else
                    {
                        _stackCurrent = _maxStack;
                    }
                    Debug.Log($"Stack current = {_stackCurrent}");
                    listEffect.Remove(item);
                    break;
                }
            }    
        }
    }

    public override void RemoveEffect()
    {
        base.RemoveEffect();
    }

    public override void UpdateEffect()
    {
        base.UpdateEffect();
    }
}
