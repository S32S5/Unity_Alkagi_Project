/**
 * Manage InitialSettingVariable
 * 
 * Script Explanation
 * - return initial eggnumber, firstTurn and turnTimeLimit
 * 
 * @version 0.0.3
 * - New script
 * @author S3
 * @date 2024/02/07
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
        isvPath = Path.Combine(Application.persistentDataPath + "/InitialSettingVariable.json");
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

    /*
     * Return initialEggNumber
     * 
     * @return int isv.initialEggNumber
     */
    public int GetInitialEggNumber()
    {
        return isv.initialEggNumber;
    }

    /*
     * Return InitialFirstTurn
     * 
     * @return bool isv.firstTurn
     */
    public bool GetInitialFirstTurn()
    {
        return isv.firstTurn;
    }

    /*
     * Return InitialTurnTimeLimit
     * 
     * @return string isv.turnTimeLimit
     */
    public string GetInitialTurnTimeLimit()
    {
        return isv.turnTimeLimit;
    }

    /*
     * Set initialEggNumber
     * 
     * @param int initialEggNumber
     */
    public void SetInitialEggNumber(int initialEggNumber)
    {
        isv.initialEggNumber = initialEggNumber;
        isvToJson();
    }

    /*
     * Set firstTurn
     * 
     * @param bool firstTurn
     */
    public void SetInitialFirstTurn(bool firstTurn)
    {
        isv.firstTurn = firstTurn;
        isvToJson();
    }

    /*
     * Set turnTimeLimit
     * 
     * @param string turnTimeLimit
     */
    public void SetInitialTurnTimeLimit(string turnTimeLimit)
    {
        isv.turnTimeLimit = turnTimeLimit;
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