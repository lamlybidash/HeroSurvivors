using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DailyQuestController : MonoBehaviour
{
    public static DailyQuestController Instance;
    private List<Quest> activeQuests = new List<Quest>();
    private List<Quest> quests = new List<Quest>(); // TODO phát triển game : Nhiều nhiệm vụ trong ngày
    private Quest todayQuest;
    [SerializeField] private List<DailyQuestData> _listAllQuest;
    private DateTime dateNow = DateTime.Now;
    [SerializeField] private GameController _gc;
    private SaveData _saveData;

    [SerializeField] private TextMeshProUGUI _textContent;
    [SerializeField] private TextMeshProUGUI _textProcess;
    [SerializeField] private TextMeshProUGUI _textGold;

    [System.Serializable]
    public class Quest
    {
        public DailyQuestData data;
        public bool isComplete;
        public int currentProcess;
        public string des;
        public Quest() { }
        public Quest(DailyQuestData questData)
        {
            int countTemp = 0;
            if (questData.questType == QuestType.Kill)
            {
                countTemp = UnityEngine.Random.Range(200, 501); // Randon 200-500 enemy
                questData.required = countTemp;
                questData.goldReward = (int)1.5 * countTemp;
                des = string.Format(questData.description, countTemp);
            }
            if (questData.questType == QuestType.Survive)
            {
                countTemp = UnityEngine.Random.Range(3, 11);    //Random 3-10 minutes
                questData.required = countTemp * 60;
                questData.goldReward = 100 * countTemp;
                des = string.Format(questData.description, countTemp);
            }
            if (questData.questType == QuestType.UpgradeWeapon)
            {
                countTemp = UnityEngine.Random.Range(1, 3);    //Random 1-3 weapons
                questData.required = countTemp;
                questData.goldReward = 200 * countTemp;
                des = string.Format(questData.description, countTemp);
            }
            data = questData;
            isComplete = false;
            currentProcess = 0;
        }
    }    

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DailyQuestEvent.OnEnemyKilled += HandleEnemyKilled;
        DailyQuestEvent.OnTimePassed += HandleTimePassed;
        DailyQuestEvent.OnWeaponUpgrade += HandleWeaponUpgrade;
    }

    private void Start()
    {
        GenerateDailyQuest();
    }


    #region Get Set

    public void SetupGC(GameController gcx)
    {
        _gc = gcx;
    }
    #endregion

    private void GenerateDailyQuest()
    {
        bool _isCreateNewQuest;
        DateTime parsedDate;
        _saveData = SaveDataManager.instance.GetSave();
        if (_saveData.questToday.date == "" || _saveData.questToday.date == null)
        {
            _isCreateNewQuest = true;
            parsedDate = DateTime.Now;
        }
        else
        {
            DateTime.TryParse(_saveData.questToday.date, out parsedDate);
            if (parsedDate.Date != dateNow.Date) // Nếu hôm nay là ngày khác
            {
                _isCreateNewQuest = true;
            }
            else
            {
                _isCreateNewQuest = false;
            }
        }

        if (_isCreateNewQuest)
        {
            //Tạo 1 quest cho ngày hôm nay
            todayQuest = new Quest(_listAllQuest[(dateNow.Day + 1) % _listAllQuest.Count]);
            quests.Add(todayQuest);
            _saveData.questToday.questType = todayQuest.data.questType.ToString();
            _saveData.questToday.required = todayQuest.data.required;
            _saveData.questToday.goldReward = todayQuest.data.goldReward;
            _saveData.questToday.date = dateNow.ToString("o");
            SaveDataManager.instance.SaveGame();
            SaveDataManager.instance.LoadGame();
            SetupPanel(todayQuest);
        } 
        else
        {
            QuestType typeQ = (QuestType)Enum.Parse(typeof(QuestType), _saveData.questToday.questType);
            foreach (DailyQuestData questi in _listAllQuest)
            {
                if (typeQ == questi.questType)
                {
                    questi.required = _saveData.questToday.required;
                    questi.goldReward = _saveData.questToday.goldReward;
                    todayQuest = new Quest(questi);
                    break;
                }    
            }
        }
        SetupPanel(todayQuest);
    }

    private void SetupPanel(Quest questx)
    {
        //TODO Update language
        _textContent.text = questx.des;
        _textProcess.text = $"Process: ({questx.currentProcess}/{questx.data.required})";
        _textGold.text = $"Gold Reward: {questx.data.goldReward}G";
    }    
    private void OnDestroy()
    {
        DailyQuestEvent.OnEnemyKilled -= HandleEnemyKilled;
        DailyQuestEvent.OnTimePassed -= HandleTimePassed;
        DailyQuestEvent.OnWeaponUpgrade -= HandleWeaponUpgrade;
    }
  
    public void HandleEnemyKilled()
    {
        foreach (Quest quest in activeQuests.ToArray())
        {
            if (quest.data.questType == QuestType.Kill)
            {
                quest.currentProcess++;
                if (quest.currentProcess >= quest.data.required)
                {
                    CompleteQuest(quest);
                }
            }
        }
    }
    private void CompleteQuest(Quest qx)
    {
        if (_gc != null)
        {
            _gc.TakeTotalCoin(qx.data.goldReward);
        }
        qx.isComplete = true;
        activeQuests.Remove(qx);
        //TODO Show Windows Complete
    }    
    public void HandleTimePassed(float deltaTime)
    {
        if (Time.timeScale == 0) return;

        foreach (var quest in activeQuests.ToArray())
        {
            if (quest.data.questType == QuestType.Survive)
            {
                quest.currentProcess += (int)deltaTime;
                if (quest.currentProcess >= quest.data.required)
                {
                    CompleteQuest(quest);
                }
            }
        }
    }
    public void HandleWeaponUpgrade()
    {
        foreach (var quest in activeQuests.ToArray())
        {
            if (quest.data.questType == QuestType.UpgradeWeapon)
            {
                quest.currentProcess++;
                if (quest.currentProcess >= quest.data.required)
                {
                    CompleteQuest(quest);
                }
            }
        }
    }
    public void StartQuest()
    {
        activeQuests.Clear();
        foreach(var item in quests)
        {
            if(item.isComplete == false)
            {
                item.currentProcess = 0;
                activeQuests.Add(item);
            } 
        }    
    }    
}