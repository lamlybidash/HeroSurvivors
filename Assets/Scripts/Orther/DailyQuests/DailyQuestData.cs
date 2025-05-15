using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType { Kill, Survive, UpgradeWeapon }
[CreateAssetMenu(menuName = "GameData/Create Daily Quest Data", fileName = "DailyQuestData")]
public class DailyQuestData : ScriptableObject
{
    public string questName;
    public QuestType questType;
    [TextArea] public string description; // Sinh tồn {0} phút trong một trận đấu
    public int required;
    public int goldReward;
}
