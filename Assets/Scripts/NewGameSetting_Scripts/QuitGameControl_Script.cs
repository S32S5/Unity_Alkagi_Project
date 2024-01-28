/**
 * Manages related to quit game
 * 
 * Script Explanation
 * - Quit game
 * - Return QuitGame_Panel is active
 * 
 * @version 0.0.2
 *  -Code optimization
 * @author S3
 * @date 2024/01/28
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
        QuitGameYes_Button.onClick.AddListener(QuitGame);
        QuitGameNo_Button = GameObject.Find("QuitGameNo_Button").GetComponent<Button>();
        QuitGameNo_Button.onClick.AddListener(QuitGameCancel);

        QuitGameBackground_Panel.SetActive(false);
    }

    // Show QuitGame_Panel
    public void ShowQuitGame_Panel()
    {
        QuitGameBackground_Panel.SetActive(true);
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Quit game cancel
    public void QuitGameCancel()
    {
        QuitGameBackground_Panel.SetActive(false);
    }

    /*
     * Return QuitGame_Panel is active
     * 
     * @return true or false
     */
    public bool QuitGame_Panel_EnDis()
    {
        return QuitGameBackground_Panel.activeSelf;
    }
}