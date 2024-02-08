/**
 * Manages related to preferences
 * 
 * @version 0.0.3, New script
 * @author S3
 * @date 2024/02/07
*/

using UnityEngine;
using UnityEngine.UI;

public class Preferences_Script : MonoBehaviour
{
    private Button Preferences_Button, PreferencesClose_Button;

    // Specifies
    private void Awake()
    {
        Preferences_Button = GameObject.Find("Preferences_Button").GetComponent<Button>();
        PreferencesClose_Button = GameObject.Find("PreferencesClose_Button").GetComponent <Button>();

        gameObject.AddComponent<GameResolution_Script>();
        gameObject.AddComponent<ScreenMode_Script>();
        gameObject.AddComponent<VolumeController>();
    }

    // Specifies when game start
    private void Start()
    {
        Preferences_Button.onClick.AddListener(() => gameObject.SetActive(true));
        PreferencesClose_Button.onClick.AddListener(() => gameObject.SetActive(false));

        gameObject.SetActive(false);
    }

    /*
     * Set PreferencesBackground_Panel
     * 
     * @param bool OnOff
     */
    public void SetPreferencesBackground_Panel(bool OnOff)
    {
        gameObject.SetActive(OnOff);
    }

    /*
     * Return PreferencesBackground_Panel is active
     * 
     * @return bool PreferencesBackground.IsActive()
     */
    public bool GetPreferencesBackground_Panel_IsOn()
    {
        return gameObject.activeSelf;
    }
}