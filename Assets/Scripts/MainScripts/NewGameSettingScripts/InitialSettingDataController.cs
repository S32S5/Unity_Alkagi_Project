/**
 * Control initialSettingData
 * 
 * @version 1.1.0
 * - Delete gameMode
 * @author S3
 * @date 2024/03/09
*/

using System;
using System.IO;
using UnityEngine;

public class InitialSettingDataController : MonoBehaviour
{
    private InitialSettingData data;
    private string path;

    private void Awake()
    {
        path = Path.Combine(Application.dataPath + "/InitialSettingData.json");
        LoadData();
    }

    private void LoadData()
    {
        string jsonStr = File.ReadAllText(path);
        data = JsonUtility.FromJson<InitialSettingData>(jsonStr);
    }

    // Set eggNum
    // 
    // @param int
    public void SetEggNum(int eggNum)
    {
        data.eggNum = eggNum;
        DataToJson();
    }

    // Return eggNum
    // 
    // @return int
    public int GetEggNum() { return data.eggNum; }

    // Set firstTurn
    // 
    // @param bool
    public void SetFirstTurn(bool firstTurn)
    {
        data.firstTurn = firstTurn;
        DataToJson();
    }

    // Return firstTurn
    // 
    // @return bool
    public bool GetFirstTurn() { return data.firstTurn; }

    // Set timeLimit
    // 
    // @param string
    public void SetTimeLimit(string timeLimit)
    {
        data.timeLimit = timeLimit;
        DataToJson();
    }

    // Return timeLimit
    //
    // @return string
    public string GetTimeLimit() { return data.timeLimit; }

    private void DataToJson()
    {
        string jsonStr = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonStr);
    }
}

[Serializable]
class InitialSettingData
{
    public int eggNum;
    public bool firstTurn;
    public string timeLimit;
}