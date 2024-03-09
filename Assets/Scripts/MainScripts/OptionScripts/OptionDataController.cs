/**
 * Manage PreferencesData
 * 
 * @version 1.0.0
 * - Change class name PreferencesData_Script to OptionDataController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using System;
using System.IO;
using UnityEngine;

public class OptionDataController : MonoBehaviour
{
    private OptionData data;
    private string path;

    private void Awake()
    {
        path = Path.Combine(Application.dataPath + "/OptionData.json");
        LoadData();
    }

    private void LoadData()
    {
        string jsonStr = File.ReadAllText(path);
        data = JsonUtility.FromJson<OptionData>(jsonStr);
    }

    // Set bgm volume
    //
    //@param float
    public void SetBgmVolume(float bgmVolume)
    {
        data.bgmVolume = bgmVolume;
        DataToJson();
    }

    // Return bgm volume
    //
    // @return float
    public float GetBgmVolume() { return data.bgmVolume; }

    //Set se volume
    //
    // @param float
    public void SetSeVolume(float seVolume)
    {
        data.seVolume = seVolume;
        DataToJson();
    }

    // Return se volume
    //
    // @return float
    public float GetSeVolume() { return data.seVolume; }

    private void DataToJson()
    {
        string jsonStr = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonStr);
    }
}

[Serializable]
class OptionData
{
    public float bgmVolume;
    public float seVolume;
}