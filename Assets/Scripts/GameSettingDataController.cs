/**
 * Control game setting data
 * 
 * @version 1.1.0
 * - Change class name InitialSettingDataController to GameSettingDataController
 * - Delete gameMode
 * @author S3
 * @date 2024/03/10
*/

using System;
using System.IO;
using UnityEngine;

public class GameSettingDataController : MonoBehaviour
{
    private GameSettingData data;
    private string path;

    private void Awake()
    {
        path = Path.Combine(Application.dataPath + "/GameSettingData.json");
        LoadData();
    }

    private void LoadData()
    {
        string jsonStr = File.ReadAllText(path);
        data = JsonUtility.FromJson<GameSettingData>(jsonStr);
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
class GameSettingData
{
    public int eggNum;
    public bool firstTurn;
    public string timeLimit;
}