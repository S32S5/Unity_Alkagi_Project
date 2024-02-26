/**
 * Manage PreferencesData
 * 
 * @version 0.0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/16
*/

using System;
using System.IO;
using UnityEngine;

public class PreferencesData_Script : MonoBehaviour
{
    private string pdPath;

    private PreferencesData pd;

    // Load json file
    private void Awake()
    {
        pdPath = Path.Combine(Application.dataPath + "/PreferencesData.json");
        if (!File.Exists(pdPath))
        {
            pd = new PreferencesData();
            pd.gameResolution = 0;
            pd.bgmVolume = 0.5f;
            pd.seVolume = 0.5f;

            string str = JsonUtility.ToJson(pd, true);
            File.WriteAllText(pdPath, str);
        }
        else
        {
            string str = File.ReadAllText(pdPath);
            pd = JsonUtility.FromJson<PreferencesData>(str);
        }
    }

    // Return game resolution
    //
    // @return int gameResolution
    public int GetGameResolution() { return pd.gameResolution; }

    // Set game resolution
    //
    // @param int grIndex
    public void SetGameResolution(int grIndex)
    {
        pd.gameResolution = grIndex;
        pdToJson();
    }

    // Return bgm volume
    //
    // @return float bgmVolume
    public float GetBgmVolume() { return pd.bgmVolume; }

    // Set bgm volume
    //
    //@param float bgmVolume
    public void SetBgmVolume(float bgmVolume)
    {
        pd.bgmVolume = bgmVolume;
        pdToJson();
    }

    // Return se volume
    //
    // @return float seVolume
    public float GetSeVolume() { return pd.seVolume; }

    //Set se volume
    //
    // @param float seVolume
    public void SetSeVolume(float seVolume)
    {
        pd.seVolume = seVolume;
        pdToJson();
    }

    // pd to json
    private void pdToJson()
    {
        string str = JsonUtility.ToJson(pd, true);
        File.WriteAllText(pdPath, str);
    }
}

[Serializable]
class PreferencesData
{
    public int gameResolution;
    public float bgmVolume;
    public float seVolume;
}