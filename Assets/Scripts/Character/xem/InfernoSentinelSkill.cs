using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoSentinelSkill : Skill
{
    private Health _healthPlayer;
    private float _duration;
    private float _scaleMultiDame = 2f; //
    private float _distanceMax = 3.5f;
    private int MaxStack = 10;
    private void Start()
    {
        _id = "inferno_sentinel";
        _CDs = 40f;
        _duration = 5f; //Thời gian bất tử
        _typeSkill = 'Q';
        _imgArea.transform.localScale = new Vector3(_distanceMax * 2, _distanceMax * 2, 1);
        InitData();
    }

    private IEnumerator InfernoSentine()
    {
        _healthPlayer = _player.GetComponent<Health>();
        _healthPlayer.SetCanDie(false);
        yield return new WaitForSeconds(_duration);
        _healthPlayer.SetCanDie(true);
    }
    //TODO: Tạo effect bất tử và sử dụng thay vì sử dụng cách trên

    public override void UseSkill()
    {
        if (_skillCDs.CanUseSkill() == false)
        {
            return;
        }
        _skillCDs.UseSkill();
        StartCoroutine(InfernoSentine());
    }
}
