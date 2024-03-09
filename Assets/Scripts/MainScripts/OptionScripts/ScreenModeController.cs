/**
 * Control screen mode's toggles
 * 
 * @version 1.0.0
 * - Change class name ScreenMode_Script to ScreenModeController
 * - Code optimization
 * @author S3
 * @date 2024/03/06
*/

using UnityEngine;
using UnityEngine.UI;

public class ScreenModeController : MonoBehaviour, OptionContent
{
    Toggle fullScreen, windowMode;

    private void Awake()
    {
        fullScreen = GameObject.Find("FullScreenModeToggle").GetComponent<Toggle>();
        windowMode = GameObject.Find("WindowModeToggle").GetComponent<Toggle>();
    }

    // Init when game start
    public void Start() { Init(); }

    public void Init()
    {
        if (Screen.fullScreen)
            fullScreen.isOn = true;
        else
            windowMode.isOn = true;
    }

    public void FullScreenOnValueChanged() { Screen.fullScreen = true; }

    public void WindowModeOnValueChanged() { Screen.fullScreen = false; }
}