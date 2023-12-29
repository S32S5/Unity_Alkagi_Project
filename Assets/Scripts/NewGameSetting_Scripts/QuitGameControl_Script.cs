/**
 * Manages related to quit game
 * 
 * Script Explanation
 * - Quit game
 * - Return QuitGame_Panel is active
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/27
*/

using UnityEngine;
using UnityEngine.UI;

public class QuitGameControl_Script : MonoBehaviour
{
    private GameObject QuitGame_Panel;
    private Button QuitGameYes_Button, QuitGameNo_Button;

    /*
     * Specifies
     */
    private void Start()
    {
        QuitGame_Panel = GameObject.Find("NewGame_Canvas").transform.Find("QuitGame_Panel").gameObject;

        GameObject QuitGameButton_Panel = QuitGame_Panel.transform.Find("QuitGameButton_Panel").gameObject;
        QuitGameYes_Button = QuitGameButton_Panel.transform.Find("QuitGameYes_Button").GetComponent<Button>();
        QuitGameNo_Button = QuitGameButton_Panel.transform.Find("QuitGameNo_Button").GetComponent<Button>();

        QuitGameYes_Button.onClick.AddListener(QuitGame);
        QuitGameNo_Button.onClick.AddListener(QuitGameCancel);
    }

    /*
     * Show QuitGame_Panel
     */
    public void ShowQuitGame_Panel()
    {
        QuitGame_Panel.SetActive(true);
    }

    /*
     * Quit game
     */
    public void QuitGame()
    {
        Application.Quit();
    }

    /*
     * Quit game cancel
     */
    public void QuitGameCancel()
    {
        QuitGame_Panel.SetActive(false);
    }

    /*
     * Return QuitGame_Panel is active
     * 
     * @return true or false
     */
    public bool QuitGame_Panel_EnDis()
    {
        return QuitGame_Panel.activeSelf;
    }
}
