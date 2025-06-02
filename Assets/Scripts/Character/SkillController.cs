using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    [SerializeField] private CharacterController _cc;
    [SerializeField] private Image _imgSkillE;
    [SerializeField] private Image _imgSkillQ;
    [SerializeField] private Image _imgCDsE;
    [SerializeField] private Image _imgCDsQ;
    private Skill _skillE;
    private Skill _skillQ;

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {
            _skillE.UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.Q) && Time.timeScale != 0)
        {
            _skillQ.UseSkill();
        }
    }    

    private void SetupInfor(Skill x)
    {
        Debug.Log(x.GetTypeSkill());
        if(x.GetTypeSkill() == 'E')
        {
            _skillE = x;
            _imgSkillE.sprite = x.GetImageSkill();
            _skillE.SetImageCDs(_imgCDsE);
        }

        if (x.GetTypeSkill() == 'Q')
        {
            _skillQ = x;
            _imgSkillQ.sprite = x.GetImageSkill();
            _skillQ.SetImageCDs(_imgCDsQ);
        }
    }
    public void SetupSkill()
    {
        SetupInfor(_cc.CharActive().GetComponent<Character>().GetSkill('E'));
        SetupInfor(_cc.CharActive().GetComponent<Character>().GetSkill('Q'));
        _skillE.SetPlayer(_cc.CharActive().transform);
        _skillQ.SetPlayer(_cc.CharActive().transform);
        _skillE.ResetCDs();
        _skillQ.ResetCDs();
        UpdateLanguage();
    }

    public void UpdateLanguage()
    {
        if(!_skillE || !_skillQ)
        {
            return;
        }    
        _skillE.LoadInfor();
        _skillQ.LoadInfor();
    }

    public void ShowInforSkillE()
    {
        _skillE.ShowInfor();
    }
    public void HideInforSkillE()
    {
        _skillE.HideInfor();
    }
    public void ShowInforSkillQ()
    {
        _skillQ.ShowInfor();
    }
    public void HideInforSkillQ()
    {
        _skillQ.HideInfor();
    }
}
