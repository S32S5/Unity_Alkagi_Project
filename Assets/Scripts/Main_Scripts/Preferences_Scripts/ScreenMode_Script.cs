/**
 * Manage related to screen mode
 * 
 * @version 0.0.3
 * - New script
 * @author S3
 * @date 2024/02/06
*/

using UnityEngine;
using UnityEngine.UI;

public class ScreenMode_Script : MonoBehaviour
{
    private Toggle FullScreenMode_Toggle, WindowMode_Toggle;

    // Specifies
    private void Awake()
    {
        FullScreenMode_Toggle = GameObject.Find("FullScreenMode_Toggle").GetComponent<Toggle>();
        FullScreenMode_Toggle.onValueChanged.AddListener(delegate { Screen.fullScreen = true; });
        WindowMode_Toggle = GameObject.Find("WindowMode_Toggle").GetComponent<Toggle>();
        WindowMode_Toggle.onValueChanged.AddListener(delegate { Screen.fullScreen = false; });
        if (Screen.fullScreen)
            FullScreenMode_Toggle.isOn = true;
        else
            WindowMode_Toggle.isOn = true;
    }
}