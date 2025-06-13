using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    protected string _id;
    protected string _name;
    protected string _des;
    protected string _exDes;
    protected float _CDs;
    protected char _typeSkill;  // 'E' & 'Q'
    protected Transform _player;

    private string _cdstext;

    [SerializeField] protected SkillCDs _skillCDs;
    [SerializeField] protected SpriteRenderer _imgArea;
    [SerializeField] protected Sprite _imgSkill;


    public void InitData()
    {
        _skillCDs.SetupDataSkill(_CDs);
        LoadInfor();
    }

    public virtual void SetPlayer(Transform playerx)
    {
        _player = playerx;
    }

    // for child override
    public virtual void UseSkill()
    {

    }
    public virtual void ShowInfor()
    {
        if(_imgArea)
        {
            _imgArea.gameObject.SetActive(true);
        }
        InforWindow.Instance.SetupInfor(_name, $"{_cdstext}:{_CDs}s", _des, _exDes ?? null);
        InforWindow.Instance.ShowInfor();
    }
    public virtual void HideInfor()
    {
        if (_imgArea)
        {
            _imgArea.gameObject.SetActive(false);
        }
        InforWindow.Instance.HideInfor();
    }

    public void LoadInfor()
    {
        _cdstext = LanguageManager.instance.GetText("skill", "CDs");
        _name = LanguageManager.instance.GetText("skill", _id);
        _des = LanguageManager.instance.GetText("skill", $"des_{_id}");
        _exDes = LanguageManager.instance.GetText("skill", $"exdes_{_id}");
    }    
    public void ResetCDs()
    {
        _skillCDs.ResetSkillI();
    }
    public Sprite GetSprite()
    {
        return _imgSkill;
    }
    #region GetSet

    public Sprite GetImageSkill()
    {
        return _imgSkill;
    }    

    public char GetTypeSkill()
    {
        return _typeSkill;
    }    

    public void SetImageCDs(Image imgCDsx)
    {
        _skillCDs.SetupImageCDs(imgCDsx);
    }    
    #endregion
}
