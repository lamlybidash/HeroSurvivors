using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DailyQuestEvent
{
    public static event Action OnEnemyKilled;
    public static event Action OnWeaponUpgrade;
    public static event Action<float> OnTimePassed;
    public static void TimePassed(float deltaTime) => OnTimePassed?.Invoke(deltaTime);
    public static void WeaponLvMax() => OnWeaponUpgrade?.Invoke();
    public static void EnemyKilled() => OnEnemyKilled?.Invoke();

    // Tương đương
    /*
    public static void EnemyKilled()
    {
        if(OnEnemyKilled != null)
        {
            OnEnemyKilled.Invoke();  //Chạy những hàm đăng ký OnEnemyKilled
        }    
    }
    */
}