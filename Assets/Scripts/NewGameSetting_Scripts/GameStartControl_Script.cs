/**
 * Manages related to game start
 * 
 * Script Explanation
 * - Game start action
 * - Send NewGameSetting error message
 * - Return GameSettingError_Panel is active
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/27
*/

using UnityEngine;
using UnityEngine.UI;

public class GameStartControl_Script : MonoBehaviour
{
    private GameObject GameSettingError_Panel;
    private Text GameSettingError_Text;
    private Button GameStart_Button, GameSettingErrorCheck_Button;

    /*
     * Specifies
     */
    private void Start()
    {
        GameObject NewGame_Canvas = GameObject.Find("NewGame_Canvas").gameObject;
        GameSettingError_Panel = NewGame_Canvas.transform.Find("GameSettingError_Panel").gameObject;
        
        GameSettingError_Text = GameSettingError_Panel.transform.Find("GameSettingError_Text").GetComponent<Text>();

        GameStart_Button = NewGame_Canvas.transform.Find("NewGameSetting_Panel").transform.Find("GameStart_Button").GetComponent<Button>();
        GameStart_Button.onClick.AddListener(GameStart);

        GameSettingErrorCheck_Button = GameSettingError_Panel.transform.Find("GameSettingErrorCheck_Button").GetComponent<Button>();
        GameSettingErrorCheck_Button.onClick.AddListener(NewGameSettingErrorMessageCheck);
    }

    /*
     * Check NewGameSetting and starts game
     */
    private void GameStart()
    {
        // Check turnTimeLimit is null or 5 or less
        if (GetComponent<TurnTimeLimitControl_Script>().CheckTurnTimeLimitNull())
            ShowNewGameSettingErrorMessage("�� �ð� ������ �Է��� �ּ���."); // "Please enter a time limit."
        else if(GetComponent<TurnTimeLimitControl_Script>().GetTurnTimeLimit() < 5)
            ShowNewGameSettingErrorMessage("�� �ð� ������ 5���� 999999999���̷� �Է��� �ּ���"); // "Please enter a time limit of 5 to 999999999"
        else
        {
            GetComponent<NewGameSetting_Script>().SetNewGameSetting_Panel(false);
            GetComponent<Main_Director_Script>().enableInGame();
        }
    }

    /*
     * Send error message
     * 
     * @param string error message
     */
    private void ShowNewGameSettingErrorMessage(string message)
    {
        GameSettingError_Panel.SetActive(true);
        GameSettingError_Text.text = message;
    }

    /*
     * GameSettingError_Panel Setactive(false)
     */
    public void NewGameSettingErrorMessageCheck()
    {
        GameSettingError_Panel.SetActive(false);
    }

    /*
     * Return GameSettingError_Panel is active
     * 
     * @return true or false
     */
    public bool GameSettingError_Panel_EnDis()
    {
        return GameSettingError_Panel.activeSelf;
    }
}
