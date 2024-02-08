/**
 * Manage PreferencesData
 * 
 * @version 0.0.3
 * - New script
 * @author S3
 * @date 2024/02/06
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
        pdPath = Path.Combine(Application.persistentDataPath + "/PreferencesData.json");
        if (!File.Exists(pdPath))
        {
            pd = new PreferencesData();
            pd.gameResolution = 1;
            pd.bgmVolume = 0.5f;
            pd.seVolume = 0.5f;

            string json = JsonUtility.ToJson(pd, true);
            File.WriteAllText(pdPath, json);
        }
        else
        {
            string json = File.ReadAllText(pdPath);
            pd = JsonUtility.FromJson<PreferencesData>(json);
        }
    }

    /*
     * Get game resolution
     * 
     * @return int pd.gameResolution
     */
    public int GetGameResolution()
    {
        return pd.gameResolution;
    }

    /*
     * Get bgm volume
     * 
     * @return float pd.bgmVolume
     */
    public float GetBgmVolume()
    {
        return pd.bgmVolume;
    }

    /*
     * Get se volume
     * 
     * @return float pd.seVolume
     */
    public float GetSeVolume()
    {
        return pd.seVolume;
    }

    /*
     * Set game resolution
     * 
     * @param int grIndex
     */
    public void SetGameResolution(int grIndex)
    {
        pd.gameResolution = grIndex;
        pdToJson();
    }

    /*
     * Set bgm volume
     * 
     * @param float bgmVolume
     */
    public void SetBgmVolume(float bgmVolume)
    {
        pd.bgmVolume = bgmVolume;
        pdToJson();
    }

    /*
     * Set se volume
     * 
     * @param float seVolume
     */
    public void SetSeVolume(float seVolume)
    {
        pd.seVolume = seVolume;
        pdToJson();
    }

    // pd to json
    private void pdToJson()
    {
        string json = JsonUtility.ToJson(pd, true);
        File.WriteAllText(pdPath, json);
    }
}

[Serializable]
class PreferencesData
{
    public int gameResolution;
    public float bgmVolume;
    public float seVolume;
}