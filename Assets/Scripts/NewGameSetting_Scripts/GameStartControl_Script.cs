/**
 * Manages related to game start
 * 
 * Script Explanation
 * - Game start action
 * - Send NewGameSetting error message
 * - Return GameSettingError_Panel is active
 * 
 * @version 0.0.2, First version
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

    // Specifies
    private void Awake()
    {
        GameSettingError_Panel = GameObject.Find("GameSettingError_Panel");
        GameSettingError_Text = GameObject.Find("GameSettingError_Text").GetComponent<Text>();
        GameStart_Button = GameObject.Find("GameStart_Button").GetComponent<Button>();

        GameSettingErrorCheck_Button = GameObject.Find("GameSettingErrorCheck_Button").GetComponent<Button>();
    }

    // Specifies when game start
    private void Start()
    {
        GameSettingError_Panel.SetActive(false);
        GameStart_Button.onClick.AddListener(GameStart);

        GameSettingErrorCheck_Button.onClick.AddListener(NewGameSettingErrorMessageCheck);
    }

    // Check NewGameSetting and starts game
    private void GameStart()
    {
        // Check turnTimeLimit is null or 5 or less
        if (GetComponent<TurnTimeLimitControl_Script>().CheckTurnTimeLimitNull())
            ShowNewGameSettingErrorMessage("수 시간 제한을 입력해 주세요."); // "Please enter a time limit."
        else if(GetComponent<TurnTimeLimitControl_Script>().GetTurnTimeLimit() < 5)
            ShowNewGameSettingErrorMessage("수 시간 제한을 5에서 999999999사이로 입력해 주세요"); // "Please enter a time limit of 5 to 999999999"
        else
            GameObject.Find("Main_Director").GetComponent<Main_Director_Script>().InGame();
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

    // GameSettingError_Panel Setactive(false)
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
