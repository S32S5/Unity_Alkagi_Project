/**
 * Manages related to quit game
 * 
 * Script Explanation
 * - Quit game
 * - Return QuitGame_Panel is active
 * 
 * @version 0.0.3
 *  - Move from NewGameSetting_Scripts
 * @author S3
 * @date 2024/02/01
*/

using UnityEngine;
using UnityEngine.UI;

public class QuitGameControl_Script : MonoBehaviour
{
    private GameObject QuitGameBackground_Panel;
    private Button QuitGameYes_Button, QuitGameNo_Button;

    // Specifies
    private void Awake()
    {
        QuitGameBackground_Panel = GameObject.Find("QuitGameBackground_Panel");

        QuitGameYes_Button = GameObject.Find("QuitGameYes_Button").GetComponent<Button>();
        QuitGameNo_Button = GameObject.Find("QuitGameNo_Button").GetComponent<Button>();
    }

    // Specifies when game start
    private void Start()
    {
        QuitGameYes_Button.onClick.AddListener(() => { Application.Quit(); });
        QuitGameNo_Button.onClick.AddListener(() => { QuitGameBackground_Panel.SetActive(false); });

        QuitGameBackground_Panel.SetActive(false);
    }

    /*
     * Set QuitGame_Panel
     * 
     * @param bool OnOff
     */
    public void SetQuitGame_Panel(bool OnOff)
    {
        QuitGameBackground_Panel.SetActive(OnOff);
    }

    /*
     * Return QuitGame_Panel is active
     * 
     * @return true or false
     */
    public bool GetQuitGame_Panel_IsOn()
    {
        return QuitGameBackground_Panel.activeSelf;
    }
}