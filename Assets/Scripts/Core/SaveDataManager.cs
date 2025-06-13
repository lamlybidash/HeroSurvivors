using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct quest
{
    public string questType;
    public string date;
    public int required;
    public int goldReward;
}
[System.Serializable]
public class SaveData
{
    public int coin;
    public int best_score;
    public List<string> list_character;
    public quest questToday;
    public SaveData()
    {
        coin = 3;
        best_score = 0;
        list_character = new List<string>();
    }
}

public class SaveDataManager : MonoBehaviour
{

    public static SaveDataManager instance;

    private SaveData _saveData = null;
    private string _savePath;
    private string _savePathTest;
    private const string _keySecurity = "hsvv";

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        _savePath = Path.Combine(Application.persistentDataPath, "save.json");
        _savePathTest = Path.Combine(Application.persistentDataPath, "saveTest.json");
        LoadGame();
    }

    private void Start()
    {

    }
    private string Encrypt(string data)
    {
        string dataEncrypted = "";
        int i;
        for (i = 0; i < data.Length; i++)
        {
            dataEncrypted += (char)(data[i] ^ _keySecurity[i % _keySecurity.Length]);
        }
        dataEncrypted = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(dataEncrypted));
        return dataEncrypted;
    }
    private string Decrypt(string data)
    {
        string dataTemp = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(data));
        string dataDecrypted ="";
        int i;
        for (i = 0; i < dataTemp.Length; i++)
        {
            dataDecrypted += (char)(dataTemp[i] ^ _keySecurity[i % _keySecurity.Length]);
        }
        return dataDecrypted;
    }
    public void SaveGame(SaveData save)
    {
        string saveString;
        string saveStringEncrypted;
        saveString = JsonUtility.ToJson(save, true);
        saveStringEncrypted = Encrypt(JsonUtility.ToJson(save, true));
        File.WriteAllText(_savePath, saveStringEncrypted);
        File.WriteAllText(_savePathTest, saveString);
    }
    public void SaveGame()
    {
        string saveString;
        string saveStringEncrypted;
        saveString = JsonUtility.ToJson(_saveData, true);
        saveStringEncrypted = Encrypt(JsonUtility.ToJson(_saveData, true));
        File.WriteAllText(_savePath, saveStringEncrypted);
        File.WriteAllText(_savePathTest, saveString);
    }
    public void LoadGame()
    {
        string saveString;
        string saveStringEncrypted;
        
        if (File.Exists(_savePath))
        {
            saveStringEncrypted = File.ReadAllText(_savePath);
            saveString = Decrypt(saveStringEncrypted);
            if (_saveData == null)
            {
                _saveData = JsonUtility.FromJson<SaveData>(saveString);
            }
            else
            {
                SaveData saveDTemp = JsonUtility.FromJson<SaveData>(saveString);
                _saveData.best_score = saveDTemp.best_score;
                _saveData.coin = saveDTemp.coin;
                _saveData.list_character.Clear();
                _saveData.list_character.AddRange(saveDTemp.list_character);
            }
        }
        else
        {
            // Chơi lần đầu, tạo save mới
            _saveData = new SaveData(); // Dữ liệu mặc định
            SaveGame(_saveData); // Lưu file mới
        }

    }
    public SaveData GetSave()
    {
        return _saveData;
    }    
}