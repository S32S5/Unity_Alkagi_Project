/**
 * Manage InitialSettingVariable
 * 
 * @version 0.0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/20
*/

using System;
using System.IO;
using UnityEngine;

public class InitialSettingVariable_Script : MonoBehaviour
{
    private string isvPath;
    private InitialSettingVariable isv;

    // Load json file
    private void Awake()
    {
        isvPath = Path.Combine(Application.dataPath + "/InitialSettingVariable.json");
        if (!File.Exists(isvPath))
        {
            isv = new InitialSettingVariable();
            isv.initialEggNumber = 4;
            isv.turnTimeLimit = "5";

            string json = JsonUtility.ToJson(isv, true);
            File.WriteAllText(isvPath, json);
        }
        else
        {
            string json = File.ReadAllText(isvPath);
            isv = JsonUtility.FromJson<InitialSettingVariable>(json);
        }
    }

    // Return initialEggNumber
    // 
    // @return int
    public int GetEggNum() { return isv.initialEggNumber; }

    // Set initialEggNumber
    // 
    // @param int
    public void SetEggNum(int eggNum)
    {
        isv.initialEggNumber = eggNum;
        isvToJson();
    }

    // Return InitialFirstTurn
    // 
    // @return bool
    public bool GetFirstTurn() { return isv.firstTurn; }

    // Set firstTurn
    // 
    // @param bool firstTurn
    public void SetFirstTurn(bool firstTurn)
    {
        isv.firstTurn = firstTurn;
        isvToJson();
    }

    // Return InitialTurnTimeLimit
    //
    // @return string
    public string GetTimeLimit() { return isv.turnTimeLimit; }

    // Set turnTimeLimit
    // 
    // @param string
    public void SetTimeLimit(string timeLimit)
    {
        isv.turnTimeLimit = timeLimit;
        isvToJson();
    }

    // Isv to json
    private void isvToJson()
    {
        string json = JsonUtility.ToJson(isv, true);
        File.WriteAllText(isvPath, json);
    }
}

[Serializable]
class InitialSettingVariable
{
    public int initialEggNumber;
    public bool firstTurn;
    public string turnTimeLimit;
}